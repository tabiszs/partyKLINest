import UserType from "./UserType";

interface MsalTokenClaims {
    oid: string;
    city: string;
    country: string;
    emails: string[];
    family_name: string;
    given_name: string;
    postalCode: string;
    streetAddress: string;
    extension_AccountType: UserType;
    extension_isBanned: boolean;
}

export default MsalTokenClaims;