import AccountType from "./AccountType";

interface AccountDetails {
    city: string;
    country: string;
    emails: string[];
    family_name: string;
    given_name: string;
    postalCode: string;
    streetAddress: string;
    extension_AccountType: AccountType;
}

export default AccountDetails;