import * as msal from "@azure/msal-browser";
import MsalTokenClaims from "../DataClasses/MsalTokenClaims";
import { getTokenFromMsalClaims } from "../DataClasses/Token";

const msalConfig = {
    auth: {
        clientId: process.env.REACT_APP_CLIENT_ID!,
        authority: process.env.REACT_APP_DEFAULT_USER_FLOW!,
        knownAuthorities: [process.env.REACT_APP_KNOWN_AUTHORITY!],
        redirectUri: '/'
    }
};

const msalInstance = new msal.PublicClientApplication(msalConfig);

const request = { scopes: [process.env.REACT_APP_B2C_SCOPE!] };

const toToken = (response: msal.AuthenticationResult) => {
    return getTokenFromMsalClaims(response.account?.idTokenClaims as MsalTokenClaims);
}

export const RetrieveToken = () => {
    return msalInstance.acquireTokenSilent(request).then((tokenResponse) => {
        if (tokenResponse === null) return null;
        msalInstance.setActiveAccount(tokenResponse.account);
        return toToken(tokenResponse);
    }).catch((error) => {
        console.log(error);
        return null;
    })
}

export const B2CLogin = async () => {
    try {
        const loginResponse = await msalInstance.loginPopup(request);
        if (loginResponse === null) return null;
        msalInstance.setActiveAccount(loginResponse.account);
        return toToken(loginResponse);
    } catch (err) {
        // handle error
        console.log(err);
        return null;
    }
}

export const B2CLogout = () => msalInstance.logoutPopup();

export const B2CEditProfile = async () => {
    try {
        const loginResponse = await msalInstance.loginPopup({
            ...request, 
            authority: process.env.REACT_APP_EDIT_PROFILE_USER_FLOW!
        });
        if (loginResponse === null) return null;
        return toToken(loginResponse);
    } catch (err) {
        console.log(err);
        return null;
    }
}

export const B2CDeleteAccount = async () => msalInstance.loginPopup({
    ...request,
    authority: process.env.REACT_APP_DELETE_ACCOUNT_USER_FLOW!
});

export const GetActiveAccount = () => {
    return msalInstance.getActiveAccount();
}

export const GetActiveAccountToken = () => {
    return getTokenFromMsalClaims(GetActiveAccount()?.idTokenClaims as MsalTokenClaims);
}

export const GetRequestHeaders = () => {
    return msalInstance.acquireTokenSilent(request)
           .then((tokenResponse) => {
               return { 
                   'Authorization': 'Bearer ' + tokenResponse?.accessToken,
                   'Content-Type': 'application/json'
                };
           });
}
