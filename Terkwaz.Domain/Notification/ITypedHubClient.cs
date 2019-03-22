namespace Terkwaz.Domain.Notification
{
    using System.Threading.Tasks;

    public interface ITypedHubClient
    {
        Task BroadcastNotification(BlogNotification payload);
    }
}
