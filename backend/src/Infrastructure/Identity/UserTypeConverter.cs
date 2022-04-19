namespace PartyKlinest.Infrastructure.Identity
{
    public static class UserTypeConverter
    {
        public static UserType StringToEnum(string userType)
        {
            switch (userType)
            {
                case "Client":
                    return UserType.Client;
                case "Cleaner":
                    return UserType.Cleaner;
                case "Organizer":
                    return UserType.Administrator;
                default:
                    throw new ArgumentException("invalid user type");
            }
        }

        public static string EnumToString(UserType userType)
        {
            switch (userType)
            {
                case UserType.Client:
                    return "Client";
                case UserType.Cleaner:
                    return "Cleaner";
                case UserType.Administrator:
                    return "Organizer";
                default:
                    throw new ArgumentException("invalid user type");
            }
        }
    }
}
