namespace PartyKlinest.WebApi.Models
{
    public record NewOrderDTO
    {
        public NewOrderDTO(decimal maxPrice, 
            int minRating,
            MessLevel messLevel)
        {
            MaxPrice = maxPrice;
            MinRating = minRating;
            MessLevel = messLevel;
        }


        public decimal MaxPrice { get; init; }
    
        public int MinRating { get; init; }

        public MessLevel MessLevel { get; init; }
    }
}
