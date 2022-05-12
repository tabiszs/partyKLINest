using System.Security.Claims;

namespace PartyKlinest.WebApi.Extensions
{
    public static class UsersExtensions
    {
        public enum UserType
        {
            Unknown, // safe value for errors
            Client,
            Cleaner,
            Admin
        }

        public static UserType GetUserType(this ClaimsPrincipal user)
        {
            try
            {
                var claim = user.Claims.Where(c => c.Type == "extension_AccountType").First();

                return claim.Value switch
                {
                    "0" => UserType.Client,
                    "1" => UserType.Cleaner,
                    "2" => UserType.Admin,
                    _ => UserType.Unknown
                };
            }
            catch (ArgumentNullException)
            {
                return UserType.Unknown;
            }
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.GetUserType() == UserType.Admin;
        }

        public static bool IsCleaner(this ClaimsPrincipal user)
        {
            return user.GetUserType() == UserType.Cleaner;
        }

        public static bool IsClient(this ClaimsPrincipal user)
        {
            return user.GetUserType() == UserType.Client;
        }

        public static string GetName(this ClaimsPrincipal user)
        {
            return user.Claims.Where(c => c.Type == "given_name").First().Value;
        }

        public static string GetSurname(this ClaimsPrincipal user)
        {
            return user.Claims.Where(c => c.Type == "family_name").First().Value;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.Claims.Where(c => c.Type == "email").First().Value;
        }

        public static string GetOid(this ClaimsPrincipal user)
        {
            return user.Claims.Where(c => c.Type == "oid").First().Value;
        }

        public static bool IsBanned(this ClaimsPrincipal user)
        {
            return user.Claims.Where(c => c.Type == "isBanned").First().Value == "true";
        }
    }
}
