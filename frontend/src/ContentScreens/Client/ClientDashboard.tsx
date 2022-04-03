import AccountDetails from "../../DataClasses/AccountDetails";
import AnnouncementList from "../AnnouncementList";

interface ClientDashboardProps {
    accountDetails: AccountDetails;
}

const ClientDashboard = (props: ClientDashboardProps) => {

    return (
        <>
            <h1>{props.accountDetails.given_name} {props.accountDetails.family_name}, witaj!</h1>
            <AnnouncementList/>
        </>
    );
}

export default ClientDashboard;