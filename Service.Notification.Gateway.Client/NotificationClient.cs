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
    
    public NotificationClient(string? uri = null)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(uri ?? $"http://{GatewayName}");
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public async Task SengMessage(MessageDto dto)
    {
        var request = RequestBuilder
            .Create("api/v1/notifications", HttpMethod.Post, _httpClient)
            .WithDataFromBodyAsJson(dto)
            .Build();

        try
        {
            await request.SendAsync();
        }
        catch (Exception e)
        {
            //
        }
        
        
    }
}


