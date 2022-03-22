import * as msal from "@azure/msal-browser";

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
    // return msalInstance.handleRedirectPromise().then((tokenResponse) => {
    //     if (tokenResponse === null) { console.log("undefined"); return undefined;}
    //     else return tokenResponse.accessToken;
    // }).catch((error) => {
    //     console.log(error);
    //     return undefined;
    // });

    // NIE DZIAŁA

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

    // return msalInstance.handleRedirectPromise().then((tokenResponse) => {
    //     if (tokenResponse === null) return undefined;
    //     else return tokenResponse.accessToken;
    // }).catch((error) => {
    //     console.log(error);
    //     return undefined;
    // });
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

export const GetActiveAccount = () => {
    // const accs = msalInstance.getAllAccounts();
    // console.log(accs);
    // return accs[0];
    return msalInstance.getActiveAccount();
}

