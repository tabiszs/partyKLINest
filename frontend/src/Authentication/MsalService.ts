import * as msal from "@azure/msal-browser";
import AccountDetails from "../DataClasses/AccountDetails";

const msalConfig = {
    auth: {
        clientId: 'e1234a8c-f3d3-4bec-ae16-7168aeafd19c',
        authority: 'https://partyklinest.b2clogin.com/partyklinest.onmicrosoft.com/B2C_1_PARTYKLINEST_USER_FLOW',
        knownAuthorities: ['https://partyklinest.b2clogin.com'],
        redirectUri: 'http://localhost:3000/'
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
        const loginResponse = await msalInstance.loginPopup({scopes:[], authority:'https://partyklinest.b2clogin.com/partyklinest.onmicrosoft.com/B2C_1_EDIT_PROFILE'});
        if (loginResponse === null) return null;
        return loginResponse.account;
    } catch (err) {
        console.log(err);
        return null;
    }
}

export const B2CDeleteAccount = async () => msalInstance.loginPopup({
    scopes:[],
    authority:'https://partyklinest.b2clogin.com/partyklinest.onmicrosoft.com/B2C_1A_DELETE_ACCOUNT'
});

export const GetActiveAccount = () => {
    return msalInstance.getActiveAccount();
}

export const GetActiveAccountDetails = () => {
    return GetActiveAccount()?.idTokenClaims as AccountDetails;
}

