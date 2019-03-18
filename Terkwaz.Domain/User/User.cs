
namespace Terkwaz.Domain.User
{
    using System.Collections.Generic;
    using Blog;

    public class User
    {
        public long Id { get; private set; }

        public string FullName { get; private set; }

        public string Email { get; private set; }

        public string PhotoUrl { get; private set; }

        public byte[] PasswordHash { get; private set; }

        public byte[] PasswordSalt { get; private set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        public static User New(string fullName, string email, string photoUrl)
        {
            return new User
            {
                FullName = fullName,
                Email = email,
                PhotoUrl = photoUrl
            };
        }


        public void UpdatePasswordHashAndSalt(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
