namespace Terkwaz.Api.Models.Identity
{
    public class UserRegisterationOutputModel
    {
        public long UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhotoUrl { get; set; }

        public string Token { get; set; }
    }
}
