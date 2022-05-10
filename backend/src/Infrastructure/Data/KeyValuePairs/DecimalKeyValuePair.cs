namespace PartyKlinest.Infrastructure.Data.KeyValuePairs
{
    internal record DecimalKeyValuePair
    {
        public DecimalKeyValuePair(string id, decimal value)
        {
            Id = id;
            Value = value;
        }

        public string Id { get; set; }
        public decimal Value { get; set; }
    }
}
