using Microsoft.Graph;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Extensions
{
    public static class GraphUserExtensions
    {
        public static string GetEmail(this User user) => user.Identities.Where(o => o.SignInType == "emailAddress").Select(o => o.IssuerAssignedId).FirstOrDefault("");

        public static UserType GetUserTypeFromProperty(this User user, string propertyName)
        {
            if (user.AdditionalData == null
                || !user.AdditionalData.TryGetValue(propertyName, out var graphAccountTypeObject)) return UserType.Client;
            return graphAccountTypeObject.ToString() switch
            {
                "Client" => UserType.Client,
                "Cleaner" => UserType.Cleaner,
                "Administrator" => UserType.Administrator,
                _ => UserType.Client
            };
        }

        public static bool GetIsBannedFromProperty(this User user, string propertyName)
        {
            if (user.AdditionalData == null
                || !user.AdditionalData.TryGetValue(propertyName, out var graphIsBannedObject)) return false;
            return graphIsBannedObject?.ToString()?.ToLower() == "true";
        }
    }
}
