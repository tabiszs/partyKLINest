using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Interfaces;

public interface IClientService
{
    /// <summary>
    /// Gets clients rating or null if client doesn't exist or 
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public Task<double?> GetClientRating(string clientId);
}