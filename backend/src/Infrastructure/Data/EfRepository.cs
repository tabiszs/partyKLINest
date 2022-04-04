using Ardalis.Specification.EntityFrameworkCore;
using PartyKlinest.ApplicationCore.Interfaces;

namespace PartyKlinest.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(PartyKlinerDbContext context) : base(context)
        {

        }
    }
}
