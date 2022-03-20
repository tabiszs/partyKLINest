using System.ComponentModel.DataAnnotations;

namespace PartyKlinest.WebApi.Models
{
    public record OrderDTO
    {
        public OrderDTO(
            long id, string clientId, string cleanerId,
            OrderStatus status, decimal maxPrice, 
            int minRating, MessLevel messLevel)
        {
            Id = id;
            ClientId = clientId;
            CleanerId = cleanerId;
            Status = status;
            MaxPrice = maxPrice;
            MinRating = minRating;
            MessLevel = messLevel;
        }

        public long Id { get; init; }

        [Required]
        public string ClientId { get; init; }
        [Required]
        public string CleanerId { get; init; }
        public OrderStatus Status { get; init; }
        public decimal MaxPrice { get; init; }
        public int MinRating { get; init; }
        public MessLevel MessLevel { get; init; }
    }
}
