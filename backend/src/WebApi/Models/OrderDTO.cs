using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;

namespace PartyKlinest.WebApi.Models
{
    public class OrderDTO
    {
        public long Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string ClientId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? CleanerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal MaxPrice { get; set; }
        public int MinRating { get; set; }
        public DateTimeOffset Date { get; set; }
        public MessLevel MessLevel { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Address Address { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Opinion? OpinionFromClient { get; set; }
        public Opinion? OpinionFromCleaner { get; set; }
    }
}
