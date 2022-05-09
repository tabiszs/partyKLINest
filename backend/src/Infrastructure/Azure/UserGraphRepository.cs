using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KLINer.Domain;
using KLINer.Domain.Exceptions;
using KLINer.Domain.Models;
using Microsoft.Graph;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Interfaces;

namespace PartyKlinest.Infrastructure.Azure
{
    public class UserGraphRepository : IRepository<Client>
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly string _extensionId;

        public UserGraphRepository(GraphServiceClient graphServiceClient, string extensionId)
        {
            _graphServiceClient = graphServiceClient;
            _extensionId = extensionId;
        }

        public async Task<List<UserInfo>> GetAllUsers()
        {
            var users = new List<UserInfo>();
            var result = await _graphServiceClient.Users.Request()
                .Select($"extension_{_extensionId}_AccountType,extension_{_extensionId}_isBanned" +
                        $",Id,GivenName,Surname,otherMails,Mail")
                .GetAsync();

            do
            {
                foreach (var user in result.CurrentPage)
                {
                    var mail = (user.OtherMails != null && user.OtherMails.Any()) ? user.OtherMails.First() : user.Mail;

                    var accountType = (user.AdditionalData != null && user.AdditionalData.ContainsKey($"extension_{_extensionId}_AccountType"))
                        ? ((JsonElement)user.AdditionalData[$"extension_{_extensionId}_AccountType"]).GetString()
                        : "UNDEFINED";
                    var isBanned = user.AdditionalData != null
                                   && user.AdditionalData.ContainsKey($"extension_{_extensionId}_isBanned")
                                   && ((JsonElement)user.AdditionalData[$"extension_{_extensionId}_isBanned"]).GetBoolean();

                    var userInfo = new UserInfo()
                    {
                        Oid = user.Id,
                        Name = user.GivenName,
                        Surname = user.Surname,
                        Email = mail,
                        AccountType = accountType,
                        IsBanned = isBanned
                    };

                    users.Add(userInfo);
                }
            } while (result.NextPageRequest != null && (result = await result.NextPageRequest.GetAsync()).Count > 0);

            return users;
        }

        public async Task BanUser(string id)
        {
            try
            {
                var user = await _graphServiceClient.Users[id].Request()
                    .Select($"extension_{_extensionId}_AccountType")
                    .GetAsync();

                var accountType = (user.AdditionalData != null && user.AdditionalData.ContainsKey($"extension_{_extensionId}_AccountType"))
                    ? ((JsonElement)user.AdditionalData[$"extension_{_extensionId}_AccountType"]).GetString()
                    : "UNDEFINED";

                var patch = new User()
                {
                    AdditionalData = new Dictionary<string, object>()
                    {
                        {$"extension_{_extensionId}_isBanned", true},
                        {$"extension_{_extensionId}_AccountType", accountType}
                    }
                };

                await _graphServiceClient.Users[id].Request().UpdateAsync(patch);
            }
            catch (ServiceException)
            {
                throw new UserNotFoundException();
            }
        }

        public async Task DeleteUser(string id)
        {
            try
            {
                await _graphServiceClient.Users[id].Request().DeleteAsync();
            }
            catch (ServiceException)
            {
                throw new UserNotFoundException();
            }
        }
    }
}
