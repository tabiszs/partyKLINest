
namespace PartyKlinest.ApplicationCore.Entities
{
    public class OrderFilter
    {
        public MessLevel MaxMessLevel { get; private set; }
        public int MinClientRating { get; private set; }
        public decimal MinPrice { get; private set; }

        public OrderFilter(MessLevel messLevel, int minClientRating, decimal minPrice)
        {
            MaxMessLevel = messLevel;
            MinClientRating = minClientRating;
            MinPrice = minPrice;
        }
    }
}
