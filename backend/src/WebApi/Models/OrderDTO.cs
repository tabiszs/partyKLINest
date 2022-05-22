using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.WebApi.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class OrderDTO
    {
        public long Id { get; set; }
        public string ClientId { get; set; }
        public string? CleanerId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal MaxPrice { get; set; }
        public int MinRating { get; set; }
        public DateTimeOffset Date { get; set; }
        public MessLevel MessLevel { get; set; }
        public AddressDTO Address { get; set; }
        public OpinionDTO? OpinionFromClient { get; set; }
        public OpinionDTO? OpinionFromCleaner { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
