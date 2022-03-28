import Button from "@mui/material/Button";
import { useState } from "react";
import { B2CEditProfile, GetActiveAccountDetails } from "../Authentication/MsalService";
import AccountDetails from "../DataClasses/AccountDetails";
import AccountType from "../DataClasses/AccountType";
import ClientDashboard from "./Client/ClientDashboard";

const Dashboard = () => {

    const [accountDetails,setAccountDetails] = useState(GetActiveAccountDetails());

    switch (accountDetails.extension_AccountType) {
        case AccountType.Client:
            return <ClientDashboard accountDetails={accountDetails}/>;
        default:
            return <></>;
    }
}

export default Dashboard;