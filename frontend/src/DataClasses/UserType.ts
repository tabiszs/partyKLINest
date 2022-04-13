// note that azure uses "0", "1", "2" instead

import AccountType from "./AccountType";

// needs to integrate with another branch
enum UserType {
    Client = "Client",
    Cleaner = "Cleaner",
    Administrator = "Administrator"
}

export const getUserTypeFromAccountType = (accountType: AccountType): UserType => {
    switch (accountType)
    {
        case AccountType.Client: return UserType.Client;
        case AccountType.Cleaner: return UserType.Cleaner;
        case AccountType.Organisator: return UserType.Administrator;
    }
}

export default UserType;