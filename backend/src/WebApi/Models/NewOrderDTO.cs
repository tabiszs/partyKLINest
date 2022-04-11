using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;

namespace PartyKlinest.WebApi.Models
{
    public record NewOrderDTO
    {
        public NewOrderDTO(decimal maxPrice,
            int minRating,
            MessLevel messLevel,
            DateTimeOffset date,
            string clientId,
            Address address)
        {
            MaxPrice = maxPrice;
            MinRating = minRating;
            MessLevel = messLevel;
            Date = date;
            ClientId = clientId;
            Address = address;
        }


        public decimal MaxPrice { get; init; }

        public int MinRating { get; init; }

        public MessLevel MessLevel { get; init; }

        public DateTimeOffset Date { get; init; }

        public string ClientId { get; init; }

        public Address Address { get; init; }
    }
}
