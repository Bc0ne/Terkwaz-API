namespace Terkwaz.Domain.User
{
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User> RegisterAsync(User user, string password);
    }
}
