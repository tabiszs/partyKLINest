import Address from "./Address";
import UserType from "./UserType";

// needs to integrate with another branch
interface Token {
    oid: string;
    userType: UserType;
    email: string;
    name: string;
    surname: string;
    address: Address;
    isBanned: boolean;
}

export default Token;