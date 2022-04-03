import Button from "@mui/material/Button";
import { useState } from "react";
import { B2CDeleteAccount, B2CEditProfile, GetActiveAccountDetails } from "../Authentication/MsalService";
import AccountDetails from "../DataClasses/AccountDetails";
import AccountType from "../DataClasses/AccountType";
import ClientSettings from "./Client/ClientSettings";

interface SettingsProps {
    logout: () => void;
}

const Settings = (props: SettingsProps) => {

    const [accountDetails,setAccountDetails] = useState(GetActiveAccountDetails());

    const editProfileHandler = () => {
        B2CEditProfile().then((acc) => {
            if (acc !== null) setAccountDetails(acc?.idTokenClaims as AccountDetails);
        });
    }

    const deleteProfileHandler = () => {
        B2CDeleteAccount().then(() => props.logout())
        .catch((error) => console.log(error));
    }

    switch (accountDetails.extension_AccountType) {
        case AccountType.Client:
            return <ClientSettings accountDetails={accountDetails} editProfile={editProfileHandler} deleteProfile={deleteProfileHandler}/>;
        default:
            return <></>;
    }
}

export default Settings;