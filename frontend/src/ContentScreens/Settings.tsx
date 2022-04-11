import { useState } from "react";
import { B2CDeleteAccount, B2CEditProfile, GetActiveAccountToken } from "../Authentication/MsalService";
import MsalTokenClaims from "../DataClasses/MsalTokenClaims";
import ClientSettings from "./Client/ClientSettings";
import { getTokenFromMsalClaims } from "../DataClasses/Token";
import UserType from "../DataClasses/UserType";

interface SettingsProps {
    logout: () => void;
}

const Settings = (props: SettingsProps) => {

    const [token, setToken] = useState(GetActiveAccountToken());

    const editProfileHandler = () => {
        B2CEditProfile().then((acc) => {
            if (acc !== null) setToken(acc);
        });
    }

    const deleteProfileHandler = () => {
        B2CDeleteAccount().then(() => props.logout())
        .catch((error) => console.log(error));
    }

    switch (token.userType) {
        case UserType.Client:
            return <ClientSettings token={token} editProfile={editProfileHandler} deleteProfile={deleteProfileHandler}/>;
        default:
            return <></>;
    }
}

export default Settings;