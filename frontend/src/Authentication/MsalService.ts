import * as msal from "@azure/msal-browser";
import AccountDetails from "../DataClasses/AccountDetails";

const msalConfig = {
    auth: {
        clientId: process.env.REACT_APP_CLIENT_ID!,
        authority: process.env.REACT_APP_DEFAULT_USER_FLOW!,
        knownAuthorities: [process.env.REACT_APP_KNOWN_AUTHORITY!],
        redirectUri: process.env.REACT_APP_REDIRECT_URI!
    }
};

const msalInstance = new msal.PublicClientApplication(msalConfig);

export const RetrieveToken = () => {
    return msalInstance.acquireTokenSilent({scopes:[]}).then((tokenResponse) => {
        if (tokenResponse === null) return undefined;
        msalInstance.setActiveAccount(tokenResponse.account);
        return tokenResponse.accessToken;
    }).catch((error) => {
        console.log(error);
        return undefined;
    })
}

export const B2CLogin = async () => {
    try {
        const loginResponse = await msalInstance.loginPopup({scopes:[]});
        if (loginResponse === null) return undefined;
        msalInstance.setActiveAccount(loginResponse.account);
        return loginResponse.accessToken;
    } catch (err) {
        // handle error
        console.log(err);
        return undefined;
    }
}

export const B2CLogout = () => msalInstance.logoutPopup();

export const B2CEditProfile = async () => {
    try {
        const loginResponse = await msalInstance.loginPopup({
            scopes: [], 
            authority: process.env.REACT_APP_EDIT_PROFILE_USER_FLOW!
        });
        if (loginResponse === null) return null;
        return loginResponse.account;
    } catch (err) {
        console.log(err);
        return null;
    }
}

export const B2CDeleteAccount = async () => msalInstance.loginPopup({
    scopes:[],
    authority: process.env.REACT_APP_DELETE_ACCOUNT_USER_FLOW!
});

export const GetActiveAccount = () => {
    return msalInstance.getActiveAccount();
}

export const GetActiveAccountDetails = () => {
    return GetActiveAccount()?.idTokenClaims as AccountDetails;
}

