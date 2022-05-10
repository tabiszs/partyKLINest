import Token from "../../DataClasses/Token";
import CleanerOrderManagement from "../CleanerOrderManagement";

interface CleanerDashboardProps {
    token: Token;
}

const CleanerDashboard = (props: CleanerDashboardProps) => {

    return (
        <>
            <h1>{props.token.name} {props.token.surname}, witaj!</h1>
            <CleanerOrderManagement />
        </>
    );
}

export default CleanerDashboard;
