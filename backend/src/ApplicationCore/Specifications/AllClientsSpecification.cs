using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Users;

namespace PartyKlinest.ApplicationCore.Specifications
{
    internal class AllClientsSpecification : Specification<Client>
    {
        public AllClientsSpecification()
        {
            Query.Where(o => true);
        }
    }
}