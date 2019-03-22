namespace Terkwaz.Web.Api.Notifications
{
    using Microsoft.AspNetCore.SignalR;
    using Terkwaz.Domain.Notification;

    public class NotifyHub : Hub<ITypedHubClient>
    {

    }
}
