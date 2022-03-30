namespace PartyKlinest.ApplicationCore.Entities.Orders.Opinions
{
    public record Opinion
    {
        public Opinion(int rating, string additionalInfo)
        {
            Rating = rating;
            AdditionalInfo = additionalInfo;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Opinion()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public int Rating { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
