using PartyKlinest.WebApi.Extensions;
using System.Net;
using System.Security.Claims;

namespace PartyKlinest.WebApi.Middleware
{
    public class BanBlockadeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BanBlockadeMiddleware> _logger;

        public BanBlockadeMiddleware(RequestDelegate next, ILogger<BanBlockadeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User != null)
            {
                _logger.LogDebug("Checking whether user is banned.");
                bool isBanned = IsBanned(context.User);

                if (isBanned)
                {
                    var oid = context.User.GetOid();
                    _logger.LogInformation("User {oid} tried to reach backend, but is banned.", oid);
                    await HandleBannedUserAsync(context);
                    return;
                }
                _logger.LogDebug("User is not banned.");
            }

            await _next(context);
        }

        private static bool IsBanned(ClaimsPrincipal user)
        {
            try
            {
                return user.IsBanned();
            }
            catch
            {
                return false;
            }
        }

        private static Task HandleBannedUserAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return context.Response.WriteAsync("User is banned. This incident will be reported.");
        }
    }

    public static class BanBlockadeMiddlewareExtension
    {
        /// <summary>
        /// Adds <see cref="BanBlockadeMiddleware"/> to the specified <paramref name="builder"/>, 
        /// which enables blocking banned users with 403 forbidden response.
        /// </summary>
        /// <remarks>
        /// It requires the user to have token already acquired,
        /// so it should be placed after authorization <c>app.UseAuthorization()</c>.
        /// </remarks>
        /// <returns>
        /// The original app parameter.
        /// </returns>
        public static IApplicationBuilder UseBanBlockade(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BanBlockadeMiddleware>();
        }
    }
}
