namespace Terkwaz.Api.Models.Identity
{
    public class UserOutputModel
    {
        public long UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhotoUrl { get; set; }

        public string Token { get; set; }
    }
}
