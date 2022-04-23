using Microsoft.EntityFrameworkCore;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.Infrastructure.Data;

namespace PartyKlinest.Infrastructure.Services;

internal class ClientService : IClientService
{
    public ClientService(PartyKlinerDbContext partyKlinerDbContext)
    {
        _dbContext = partyKlinerDbContext;
    }

    private readonly PartyKlinerDbContext _dbContext;
    
    public async Task<double?> GetClientRating(string clientId)
    {
        bool hasRatings = await DoesClientHaveRatings(clientId);
        
        if (!hasRatings)
        {
            return null;
        }
        
        double avg = await 
            _dbContext.Orders
                .Where(o => o.CleanersOpinion != null)
                .AverageAsync(o => o.CleanersOpinion!.Rating);

        return avg;
    }

    private Task<bool> DoesClientHaveRatings(string clientId)
    {
        return _dbContext.Orders
                .AnyAsync(o => o.ClientId == clientId && o.CleanersOpinion != null);
    }
}