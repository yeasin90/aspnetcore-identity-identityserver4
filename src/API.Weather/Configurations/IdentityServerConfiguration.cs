namespace Api.Weather.Configurations
{
    public class IdentityServerConfiguration
    {
        public string IdentityServerHost { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool UseHttps { get; set; }
    }
}
