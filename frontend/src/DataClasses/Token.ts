import Address from "./Address";
import MsalTokenClaims from "./MsalTokenClaims";
import UserType, { getUserTypeFromAccountType } from "./UserType";

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

export const getTokenFromMsalClaims = (claims: MsalTokenClaims): Token => ({
    oid: claims.oid,
    userType: getUserTypeFromAccountType(claims.extension_AccountType),
    email: claims.emails[0],
    name: claims.given_name,
    surname: claims.family_name,
    address: {
        street: claims.streetAddress, // tymczasowo
        buildingNo: "",
        city: claims.city,
        country: claims.country,
        postalCode: claims.postalCode
    },
    isBanned: claims.extension_isBanned
});

export default Token;