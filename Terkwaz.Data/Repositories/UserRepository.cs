namespace Terkwaz.Data.Repositories
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Terkwaz.Data.Context;
    using Terkwaz.Domain.User;

    public class UserRepository : IUserRepository
    {
        private readonly TerkwazDbContext _context;

        public UserRepository(TerkwazDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context is Null");
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Password is required");
            }

            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHashAndPasswordSalt(password, out passwordHash, out passwordSalt);

            user.UpdatePasswordHashAndSalt(passwordHash, passwordSalt);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHashAndPasswordSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
