import Token from "./Token";
import UserType from "./UserType";

interface UserInfo {
    oid: string;
    name: string;
    surname: string;
    email: string;
    accountType: UserType;
    isBanned: boolean;
}

export const getUserInfoFromToken = (token: Token): UserInfo => ({
    oid: token.oid,
    name: token.name,
    surname: token.surname,
    email: token.email,
    accountType: token.userType,
    isBanned: token.isBanned
});

export default UserInfo;