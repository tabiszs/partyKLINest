namespace PartyKlinest.WebApi.Models
{
    public record NewOrderDTO(
        decimal MaxPrice,
        int MinRating,
        MessLevel MessLevel
        );
}
