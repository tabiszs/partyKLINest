using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Interfaces;
using System;


namespace PartyKlinest.ApplicationCore.Entities.Orders
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public record Order : IAggregateRoot
    {
        public long OrderId { get; set; }

        public decimal MaxPrice { get; set; }
        public int MinCleanerRating { get; set; }

        public MessLevel MessLevel { get; set; }
        public OrderStatus Status { get; set; }

        public DateTimeOffset Date { get; set; }

        public long ClientId { get; set; }
        public Client Client { get; set; }

        public long? CleanerId { get; set; }
        public Cleaner? Cleaner { get; set; }

        public long AddressId { get; set; }
        public Address Address { get; set; }

        public long? ClientsOpinionId { get; set; }
        public Opinion? ClientsOpinion { get; set; }

        public long? CleanersOpinionId { get; set; }
        public Opinion? CleanersOpinion { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
