using System;
using System.Threading.Tasks;
using Service.Application;

namespace Service.Notification.Gateway.Client;

public interface INotificationClient : IDisposable
{
    Task SengMessage(MessageDto dto);
}