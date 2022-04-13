import AccountType from "./AccountType";

interface MsalTokenClaims {
    oid: string;
    city: string;
    country: string;
    emails: string[];
    family_name: string;
    given_name: string;
    postalCode: string;
    streetAddress: string;
    extension_AccountType: AccountType;
    extension_isBanned: boolean;
}

export default MsalTokenClaims;