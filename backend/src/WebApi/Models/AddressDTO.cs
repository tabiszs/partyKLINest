namespace PartyKlinest.WebApi.Models
{
    public record AddressDTO(
        string Street,
        string BuildingNo,
        int? flatNo,
        string city,
        int postalCode,
        string country
        );
    
}
