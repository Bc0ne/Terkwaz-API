namespace Terkwaz.Domain.User
{
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User> RegisterAsync(User user, string password);
        Task<User> GetUserByEmailAsync(string email);
        bool IsAuthenticatedUser(User user, string password);
        Task<User> GetUserByIdAsync(long userId);
    }
}
