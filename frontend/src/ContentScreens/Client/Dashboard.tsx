import Button from "@mui/material/Button";
import { useState } from "react";
import { B2CEditProfile, GetActiveAccount } from "../../Authentication/MsalService";


const Dashboard = () => {

    const [account,setAccount] = useState(GetActiveAccount());

    const editProfileHandler = () => {
        B2CEditProfile().then((acc) => {
            if (acc !== null) setAccount(acc);
        });
    }

    return (
        <>
            <h1>Hi {account?.name}!</h1>
            <Button onClick={editProfileHandler}>Edytuj profil</Button>
        </>
    );
}

export default Dashboard;