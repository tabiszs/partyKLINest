import Token from "../../DataClasses/Token";

interface CleanerDashboardProps {
    token: Token;
}

const CleanerDashboard = (props: CleanerDashboardProps) => {

    return (
        <>
            <h1>{props.token.name} {props.token.surname}, witaj!</h1>
        </>
    );
}

export default CleanerDashboard;