using Confluent.Kafka;
using Service.Application;

namespace Service.Notification.Core;

public class ConsumerResponseHandler : IConsumerResponseHandler
{
    private readonly ProducerConfig _config;

    public ConsumerResponseHandler(ProducerConfig config)
    {
        _config = config;
    }
    
    public async Task SendResponseAsync(NotificationResponse notificationResponse)
    {
        using var producer = new ProducerBuilder<Null, NotificationResponse>(_config)
            .SetValueSerializer(new JsonSerializer<NotificationResponse>())
            .Build();
        
        try
        {
            var deliveryResult = await producer
                .ProduceAsync(topic: Topics.ResponseTopic,
                    new Message<Null, NotificationResponse>
                    {
                        Value = notificationResponse,
                    },CancellationToken.None);
            //_logger.LogInformation($"Delivered message to {deliveryResult.Value}, Offset: {deliveryResult.Offset}");
        }
        catch (ProduceException<Null, MessageDto> e)
        {
            //_logger.LogError($"Delivery failed: {e.Error.Reason}");
        }
    }
    
}