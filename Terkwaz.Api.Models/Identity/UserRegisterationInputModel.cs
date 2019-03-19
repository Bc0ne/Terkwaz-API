
namespace Terkwaz.Api.Models.Identity
{
    //using System.ComponentModel.DataAnnotations;

    public class UserRegisterationInputModel
    {
        //[Required]
        public string FullName { get; set; }

        //[Required]
        public string Email { get; set; }

        public string PhotoUrl { get; set; }

        //[Required]
        public string password { get; set; }
    }
}
