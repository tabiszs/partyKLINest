using Ardalis.Specification;

namespace PartyKlinest.ApplicationCore.Interfaces
{
    internal interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
