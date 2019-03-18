namespace Terkwaz.Web.Api.Identity
{
    public class IdentityConfig
    {
        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public double TokenExpiryInMinutes { get; set; }
    }
}
