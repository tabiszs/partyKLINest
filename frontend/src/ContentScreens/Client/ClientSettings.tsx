import Button from "@mui/material/Button";
import AccountDetails from "../../DataClasses/AccountDetails";
import AnnouncementList from "../AnnouncementList";

interface ClientSettingsProps {
    accountDetails: AccountDetails;
    editProfile: () => void;
}

const ClientSettings = (props: ClientSettingsProps) => {

    return (
        <>
            <p><strong>Imię: </strong>{props.accountDetails.given_name}</p>
            <p><strong>Nazwisko: </strong>{props.accountDetails.family_name}</p>
            <p><strong>Adres: </strong>{props.accountDetails.streetAddress}, {props.accountDetails.postalCode} {props.accountDetails.city}</p>
            <p><strong>Kraj: </strong>{props.accountDetails.country}</p>
            <p><strong>E-mail: </strong>{props.accountDetails.emails[0]}</p>
            <Button onClick={props.editProfile}>Edytuj profil</Button>
        </>
    );
}

export default ClientSettings;