using System.ComponentModel.DataAnnotations;

namespace Terkwaz.Api.Models.Identity
{
    public class UserRegisterationInputModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhotoUrl { get; set; }

        [Required]
        public string password { get; set; }
    }
}
