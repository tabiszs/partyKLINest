// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

namespace Microsoft.Graph.Auth
{
    using Microsoft.Identity.Client;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// An <see cref="IAuthenticationProvider"/> implementation using MSAL.Net to acquire token by client credential flow.
    /// </summary>
    public class ClientCredentialProvider : IAuthenticationProvider
    {
        /// <summary>
        ///  A Scope property
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// A <see cref="IConfidentialClientApplication"/> property.
        /// </summary>
        public IConfidentialClientApplication ClientApplication { get; set; }

        /// <summary>
        /// Constructs a new <see cref=" ClientCredentialProvider"/>
        /// </summary>
        /// <param name="confidentialClientApplication">A <see cref="IConfidentialClientApplication"/> to pass to <see cref="ClientCredentialProvider"/> for authentication.</param>
        /// <param name="scope">Scope required to access Microsoft Graph. This defaults to https://graph.microsoft.com/.default when none is set.</param>
        public ClientCredentialProvider(IConfidentialClientApplication confidentialClientApplication, string scope = "https://graph.microsoft.com/.default")
        {
            Scope = scope ?? "https://graph.microsoft.com/.default";

            ClientApplication = confidentialClientApplication ?? throw new AuthenticationException(
                    new Error
                    {
                        Code = "invalidRequest",
                        Message = string.Format("{0} cannot be null.", "confidentialClientApplication")
                    });
        }

        class SimpleAuthenticationProviderOption
        {
            public bool ForceRefresh;
            public int MaxRetry = 1;
        }

        /// <summary>
        /// Adds an authentication header to the incoming request by checking the application's <see cref="TokenCache"/>
        /// for an unexpired access token. If a token is not found or expired, it gets a new one.
        /// </summary>
        /// <param name="httpRequestMessage">A <see cref="HttpRequestMessage"/> to authenticate</param>
        public async Task AuthenticateRequestAsync(HttpRequestMessage httpRequestMessage)
        {
            AuthenticationHandlerOption authenticationHandlerOption = httpRequestMessage.GetMiddlewareOption<AuthenticationHandlerOption>();
            var msalAuthProviderOption = authenticationHandlerOption?.AuthenticationProviderOption as SimpleAuthenticationProviderOption ?? new SimpleAuthenticationProviderOption();
            int retryCount = 0;
            do
            {
                try
                {
                    AuthenticationResult authenticationResult = await ClientApplication.AcquireTokenForClient(new string[] { Scope })
                        .WithForceRefresh(msalAuthProviderOption.ForceRefresh)
                        .ExecuteAsync();

                    if (!string.IsNullOrEmpty(authenticationResult?.AccessToken))
                        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(CoreConstants.Headers.Bearer, authenticationResult.AccessToken);
                    break;
                }
                catch (MsalServiceException serviceException)
                {
                    if (serviceException.ErrorCode == "temporarily_unavailable")
                    {
                        TimeSpan delay = GetRetryAfter(serviceException);
                        retryCount++;
                        // pause execution
                        await Task.Delay(delay);
                    }
                    else
                    {
                        throw new AuthenticationException(
                            new Error
                            {
                                Code = "generalException",
                                Message = "Unexpected exception occurred while authenticating the request."
                            },
                            serviceException);
                    }
                }
                catch (Exception exception)
                {
                    throw new AuthenticationException(
                            new Error
                            {
                                Code = "generalException",
                                Message = "Unexpected exception occurred while authenticating the request."
                            },
                            exception);
                }

            } while (retryCount < msalAuthProviderOption.MaxRetry);
        }

        /// <summary>
        /// Gets retry after timespan from <see cref="RetryConditionHeaderValue"/>.
        /// </summary>
        /// <param name="authProvider">An <see cref="IAuthenticationProvider"/> object.</param>
        /// <param name="serviceException">A <see cref="MsalServiceException"/> with RetryAfter header</param>
        TimeSpan GetRetryAfter(MsalServiceException serviceException)
        {
            RetryConditionHeaderValue retryAfter = serviceException.Headers?.RetryAfter;
            TimeSpan? delay = null;

            if (retryAfter != null && retryAfter.Delta.HasValue)
            {
                delay = retryAfter.Delta;
            }
            else if (retryAfter != null && retryAfter.Date.HasValue)
            {
                delay = retryAfter.Date.Value.Offset;
            }

            if (delay == null)
                throw new MsalServiceException(serviceException.ErrorCode, "Missing retry after header.");

            return delay.Value;
        }
    }
}