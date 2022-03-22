import { GetActiveAccount } from "../../Authentication/MsalService";

const Dashboard = () => {

    const account = GetActiveAccount();

    return (
        <h1>Hi {account?.name}!</h1>
    );
}

export default Dashboard;