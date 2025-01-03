using Service.Application;

namespace Service.Notification.Core;

public interface IConsumerResponseHandler
{
    Task SendResponseAsync(NotificationResponse notificationResponse);
}