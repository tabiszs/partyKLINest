using Ardalis.Specification.EntityFrameworkCore;
using PartyKlinest.ApplicationCore.Interfaces;

namespace PartyKlinest.Infrastructure.Data
{
    internal class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(PartyKlinerDbContext context) : base(context)
        {

        }
    }
}
