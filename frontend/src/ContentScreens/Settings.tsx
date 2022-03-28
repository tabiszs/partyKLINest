import Button from "@mui/material/Button";
import { useState } from "react";
import { B2CEditProfile, GetActiveAccountDetails } from "../Authentication/MsalService";
import AccountDetails from "../DataClasses/AccountDetails";
import AccountType from "../DataClasses/AccountType";
import ClientSettings from "./Client/ClientSettings";

const Settings = () => {

    const [accountDetails,setAccountDetails] = useState(GetActiveAccountDetails());

    const editProfileHandler = () => {
        B2CEditProfile().then((acc) => {
            if (acc !== null) setAccountDetails(acc?.idTokenClaims as AccountDetails);
        });
    }
    switch (accountDetails.extension_AccountType) {
        case AccountType.Client:
            return <ClientSettings accountDetails={accountDetails} editProfile={editProfileHandler}/>;
        default:
            return <></>;
    }
}

export default Settings;