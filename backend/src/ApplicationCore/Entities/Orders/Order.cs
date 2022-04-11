using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Interfaces;
using System;


namespace PartyKlinest.ApplicationCore.Entities.Orders
{
    public record Order : IAggregateRoot
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Order()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public Order(decimal maxPrice, int minCleanerRating, MessLevel messLevel,
            DateTimeOffset date, string clientId, Address address)
        {
            MaxPrice = maxPrice;
            MinCleanerRating = minCleanerRating;
            MessLevel = messLevel;
            Date = date;
            ClientId = clientId;
            Address = address;
            Status = OrderStatus.Active;
        }

        public long OrderId { get; private set; }

        public decimal MaxPrice { get; private set; }
        public int MinCleanerRating { get; private set; }

        public MessLevel MessLevel { get; private set; }
        public OrderStatus Status { get; private set; }

        public DateTimeOffset Date { get; private set; }

        public string ClientId { get; private set; }
        public Client? Client { get; private set; }

        public string? CleanerId { get; private set; }
        public Cleaner? Cleaner { get; private set; }

        public Address Address { get; private set; }

        public Opinion? ClientsOpinion { get; private set; }

        public Opinion? CleanersOpinion { get; private set; }

        public void SetCleanerId(string cleanerId)
        {
            if (cleanerId != null && Status == OrderStatus.Active && CleanerId == null)
            {
                Status = OrderStatus.InProgress;
                CleanerId = cleanerId;
            }
        }

        public void Cancel()
        {
            if (Status != OrderStatus.Closed)
            {
                Status = OrderStatus.Cancelled;
            }
        }

        public void Close()
        {
            if (Status == OrderStatus.InProgress)
            {
                Status = OrderStatus.Closed;
            }
        }

        public void SetClientsOpinion(Opinion clientsOpinion)
        {
            ClientsOpinion = clientsOpinion;
        }

        public void SetCleanersOpinion(Opinion opinion)
        {
            CleanersOpinion = opinion;
        }

        public void Modify(string clientId, string? cleanerId, OrderStatus status, decimal maxPrice, int minRating, DateTimeOffset date, Address address, MessLevel messLevel)
        {
            ClientId = clientId;
            CleanerId = cleanerId;
            Status = status;
            MaxPrice = maxPrice;
            MinCleanerRating = minRating;
            Address = address;
            Date = date;
            MessLevel = messLevel;
        }
    }
}
