using Microsoft.Graph;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.ApplicationCore.Models;
using PartyKlinest.Infrastructure.Extensions;

namespace PartyKlinest.Infrastructure.Services
{
    internal class GraphClient : IGraphClient
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly ExtensionPropertyNameBuilder _nameBuilder;

        public GraphClient(GraphServiceClient graphServiceClient, ExtensionPropertyNameBuilder nameBuilder)
        {
            _graphServiceClient = graphServiceClient;
            _nameBuilder = nameBuilder;
        }

        public async Task<List<UserInfo>> GetUserInfoAsync(List<string> userIds)
        {
            var users = await _graphServiceClient.Users.Request()
                .Select($"id,givenName,surname,identities,{_nameBuilder.GetExtensionName("AccountType")},{_nameBuilder.GetExtensionName("isBanned")}")
                .GetAsync();

            return (from user in users
                    where userIds.Contains(user.Id)
                    select new UserInfo
                    {
                        Oid = user.Id,
                        Name = user.GivenName,
                        Surname = user.Surname,
                        Email = user.GetEmail(),
                        AccountType = user.GetUserTypeFromProperty(_nameBuilder.GetExtensionName("AccountType")),
                        IsBanned = user.GetIsBannedFromProperty(_nameBuilder.GetExtensionName("isBanned"))
                    }).ToList();
        }
    }
}
