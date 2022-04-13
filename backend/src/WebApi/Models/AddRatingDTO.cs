using System.ComponentModel.DataAnnotations;

namespace PartyKlinest.WebApi.Models
{
    public record AddRatingDTO
    {
        public AddRatingDTO(int rating)
        {
            Rating = rating;
        }

        [Range(1, 10)]
        public int Rating { get; init; }
    }
}
