using Ardalis.Specification;

namespace PartyKlinest.ApplicationCore.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
