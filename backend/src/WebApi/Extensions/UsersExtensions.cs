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
                    "Client" => UserType.Client,
                    "Cleaner" => UserType.Cleaner,
                    "Administrator" => UserType.Admin,
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

        public static string GetOid(this ClaimsPrincipal user)
        {
            return user.Claims.Where(c => c.Type == "oid" || c.Type == @"http://schemas.microsoft.com/identity/claims/objectidentifier").First().Value;
        }

        public static bool IsBanned(this ClaimsPrincipal user)
        {
            var isBannedClaims = user.Claims.Where(c => c.Type == "extension_isBanned" || c.Type == @"http://schemas.microsoft.com/identity/claims/isbanned" || c.Type == "isBanned");
            if (isBannedClaims.Count() == 1 && isBannedClaims.First().Value == "true")
            {
                return true;
            }
            return false;
        }
    }
}
