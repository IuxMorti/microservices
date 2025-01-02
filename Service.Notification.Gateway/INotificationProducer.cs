namespace Service.Application.Gateway;

public interface INotificationProducer
{

    Task ProduceAsync(MessageDto message);
}