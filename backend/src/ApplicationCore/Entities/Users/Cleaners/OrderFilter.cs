namespace PartyKlinest.ApplicationCore.Entities.Users.Cleaners
{
    public record OrderFilter
    {
        public MessLevel MaxMessLevel { get; private set; }
        public int MinClientRating { get; private set; }
        public decimal MinPrice { get; private set; }

        // required by ef 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private OrderFilter()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }
        
        public OrderFilter(MessLevel messLevel, int minClientRating, decimal minPrice)
        {
            MaxMessLevel = messLevel;
            MinClientRating = minClientRating;
            MinPrice = minPrice;
        }
    }
}
