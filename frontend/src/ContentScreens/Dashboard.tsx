import { GetActiveAccountDetails } from "../Authentication/MsalService";
import AccountType from "../DataClasses/AccountType";
import ClientDashboard from "./Client/ClientDashboard";

const Dashboard = () => {

    const accountDetails = GetActiveAccountDetails();

    switch (accountDetails.extension_AccountType) {
        case AccountType.Client:
            return <ClientDashboard accountDetails={accountDetails}/>;
        default:
            return <></>;
    }
}

export default Dashboard;