using System;
using System.Net.Http;
using System.Threading.Tasks;
using Http.Request.Builder.Builder;
using Service.Application;

namespace Service.Notification.Gateway.Client;

public class NotificationClient: INotificationClient
{
    private readonly HttpClient _httpClient;
    private const string GatewayName = "notification-gateway";
    
    public NotificationClient()
    {
        _httpClient = new HttpClient();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public Task SengMessage(MessageDto dto)
    {
        throw new NotImplementedException();
    }
}


