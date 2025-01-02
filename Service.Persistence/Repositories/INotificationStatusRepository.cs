using Service.Application;

namespace Service.Persistence.Repositories;

public interface INotificationStatusRepository
{
    Task CreateOrUpdateStatus(NotificationStatusDto statusModel);
}