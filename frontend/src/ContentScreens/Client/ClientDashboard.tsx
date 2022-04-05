import Token from "../../DataClasses/Token";
import ClientOrderManagement from "../ClientOrderManagement";

interface ClientDashboardProps {
    token: Token;
}

const ClientDashboard = (props: ClientDashboardProps) => {

    return (
        <>
            <h1>{props.token.name} {props.token.surname}, witaj!</h1>
            <ClientOrderManagement/>
        </>
    );
}

export default ClientDashboard;