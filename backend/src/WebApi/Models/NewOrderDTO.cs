using PartyKlinest.ApplicationCore.Entities;

namespace PartyKlinest.WebApi.Models
{
    public record NewOrderDTO
    {
        public NewOrderDTO(decimal maxPrice,
            int minRating,
            MessLevel messLevel,
            DateTimeOffset date,
            string clientId,
            AddressDTO address)
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

        public AddressDTO Address { get; init; }
    }
}
