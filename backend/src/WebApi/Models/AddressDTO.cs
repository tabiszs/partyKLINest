using System.ComponentModel.DataAnnotations;

namespace PartyKlinest.WebApi.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public record AddressDTO
    {
        [Required]
        public string Street { get; init; }

        [Required]
        public string BuildingNo { get; init; }

        public int? FlatNo { get; init; }

        [Required]
        public string City { get; init; }

        [Required]
        public string PostalCode { get; init; }

        [Required]
        public string Country { get; init; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
