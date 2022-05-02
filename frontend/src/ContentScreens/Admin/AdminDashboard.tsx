import Token from "../../DataClasses/Token";
import Settings from "../Client/ClientSettings";

interface AdminDashboardProps {
    token: Token;
    editProfile: () => void;
    deleteProfile: () => void;
}

const AdminDashboard = (props: AdminDashboardProps) => {

    return (
        <>
            <h1>{props.token.name} {props.token.surname}, witaj!</h1>
            <Settings token={props.token} editProfile={props.editProfile} deleteProfile={props.deleteProfile}/>
        </>
    );
}

export default AdminDashboard;