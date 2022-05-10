using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Interfaces
{
    public interface ICommissionService
    {
        public Task<decimal> GetCommissionAsync();
        public Task SetCommissionAsync(decimal newCommission);
    }
}
