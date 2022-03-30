namespace PartyKlinest.ApplicationCore.Entities.Orders.Opinions
{
    public record Opinion
    {
        public long OpinionId { get; set; }
        public int Rating { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
