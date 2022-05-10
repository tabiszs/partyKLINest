import Heading from "../../Components/Heading";
import UserList from "../../Components/UserList";
import UserInfo from "../../DataClasses/UserInfo";
import UserType from "../../DataClasses/UserType";

const mockUsers: UserInfo[] = [
    {
        oid: '54cf8951-82a3-48d2-a68b-2e8147e188a1',
        name: 'Kot',
        surname: 'Filemon',
        email: 'kot.filemon@catmail.com',
        accountType: UserType.Client,
        isBanned: false
    },
    {
        oid: '213769421abcdef',
        name: 'Jan',
        surname: 'Bródka',
        email: 'brodka@mini.pw.edu.pl',
        accountType: UserType.Cleaner,
        isBanned: true
    }
];

const UserBanning = () => {

    const users = mockUsers // getAllUsers(); -> TODO
    return (
        <div>
            <Heading content="Lista użytkowników" />
            <UserList users={users} />
        </div>
    );
}

export default UserBanning;