import { useEffect, useState } from "react";
import { getAllUsers } from "../../Api/endpoints";
import Heading from "../../Components/Heading";
import UserList from "../../Components/UserList";
import UserInfo from "../../DataClasses/UserInfo";

const UserBanning = () => {

    const [users, setUsers] = useState<UserInfo[]>([]);
    
    useEffect(() => {
        getAllUsers()
        .then(setUsers)
        .catch((err) => {
            console.log(err);
            setUsers([]);
        })
    }, []);

    return (
        <div>
            <Heading content="Lista użytkowników" />
            <UserList users={users} />
        </div>
    );
}

export default UserBanning;