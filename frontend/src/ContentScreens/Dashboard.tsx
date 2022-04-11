import { GetActiveAccountToken } from "../Authentication/MsalService";
import UserType from "../DataClasses/UserType";
import ClientDashboard from "./Client/ClientDashboard";

const Dashboard = () => {

    const token = GetActiveAccountToken();

    switch (token.userType) {
        case UserType.Client:
            return <ClientDashboard token={token}/>;
        default:
            return <></>;
    }
}

export default Dashboard;