using PartyKlinest.ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Interfaces;

public interface IGraphClient
{
    public Task<List<UserInfo>> GetUserInfoAsync(List<string> userIds);
}
