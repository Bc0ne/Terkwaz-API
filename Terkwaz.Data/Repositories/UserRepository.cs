namespace Terkwaz.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
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

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public bool IsAuthenticatedUser(User user, string password)
        {
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return false;
            }

            return true;
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password can't be null or empty", "password");
            }

            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");
            }

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int idx = 0; idx < computedHash.Length; idx++)
                {
                    if (computedHash[idx] != storedHash[idx])
                    {
                        return false;
                    }
                }
            }

            return true;
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
