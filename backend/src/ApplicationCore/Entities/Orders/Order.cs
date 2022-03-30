using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Interfaces;
using System;


namespace PartyKlinest.ApplicationCore.Entities.Orders
{
    public record Order : IAggregateRoot
    {
        private Order()
        {

        }

        public Order(decimal maxPrice, int minCleanerRating, MessLevel messLevel,
            DateTimeOffset date, long clientId, Address address)
        {
            MaxPrice = maxPrice;
            MinCleanerRating = minCleanerRating;
            MessLevel = messLevel;
            Date = date;
            ClientId = clientId;
            Address = address;
        }

        public long OrderId { get; private set; }

        public decimal MaxPrice { get; private set; }
        public int MinCleanerRating { get; private set; }

        public MessLevel MessLevel { get; private set; }
        public OrderStatus Status { get; private set; }

        public DateTimeOffset Date { get; private set; }

        public long ClientId { get; private set; }
        public Client? Client { get; private set; }

        public long? CleanerId { get; private set; }
        public Cleaner? Cleaner { get; private set; }

        public Address Address { get; private set; }

        public Opinion? ClientsOpinion { get; private set; }

        public Opinion? CleanersOpinion { get; private set; }

        public void SetCleanerId(long cleanerId)
        {
            CleanerId = cleanerId;
        }

        public void SetClientsOpinion(Opinion clientsOpinion)
        {
            ClientsOpinion = clientsOpinion;
        }

        public void SetCleanersOpinion(Opinion opinion)
        {
            CleanersOpinion = opinion;
        }

        public void Modify(long clientId, long cleanerId, OrderStatus status, decimal maxPrice, int minRating, DateTimeOffset date, MessLevel messLevel)
        {
            ClientId = clientId;
            CleanerId = cleanerId;
            Status = status;
            MaxPrice = maxPrice;
            MinCleanerRating = minRating;
            Date = date;
            MessLevel = messLevel;
        }
    }
}
