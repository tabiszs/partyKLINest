import { stringify } from "querystring";
import Token from "./Token";

interface UserInfo {
    oid: string;
    name: string;
    surname: string;
    email: string;
    accountType: string;
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