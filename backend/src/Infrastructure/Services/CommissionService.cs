using Microsoft.EntityFrameworkCore;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.Infrastructure.Data;
using PartyKlinest.Infrastructure.Data.KeyValuePairs;

namespace PartyKlinest.Infrastructure.Services
{
    internal class CommissionService : ICommissionService
    {
        private readonly PartyKlinerDbContext _dbContext;
        private const string CommissionKey = "Commission";

        public CommissionService(PartyKlinerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task<DecimalKeyValuePair> GetPairAsync()
        {
            var pair =
                await _dbContext.DecimalKeyValuePairs
                .SingleAsync(x => x.Id == CommissionKey);

            if (pair == null)
            {
                throw new Exception("Commission not found. Try adding default commission to db first.");
            }

            return pair;
        }

        public async Task<decimal> GetCommissionAsync()
        {
            var pair = await GetPairAsync();
            return pair.Value;
        }

        public async Task SetCommissionAsync(decimal newCommission)
        {
            var pair = await GetPairAsync();
            pair.Value = newCommission;
            await _dbContext.SaveChangesAsync();
        }
    }
}
