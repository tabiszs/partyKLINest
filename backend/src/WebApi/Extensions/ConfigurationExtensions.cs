namespace PartyKlinest.WebApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string[] GetAllowedOrigins(this ConfigurationManager configuration)
        {
            return configuration.GetSection("AllowedOrigins").Get<string[]>();
        }
    }
}
