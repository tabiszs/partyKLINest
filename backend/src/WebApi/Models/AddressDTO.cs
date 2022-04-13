using System.ComponentModel.DataAnnotations;

namespace PartyKlinest.WebApi.Models
{
    public record AddressDTO
    {
        public AddressDTO(
            string street,
            string buildingNo,
            string city,
            string postalCode,
            string country,
            int? flatNo = null
        )
        {
            Street = street;
            BuildingNo = buildingNo;
            FlatNo = flatNo;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }


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
}
