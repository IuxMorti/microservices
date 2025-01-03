using Confluent.Kafka;
using Service.Notification.Core;

namespace Service.Application.Gateway;
public class ProducerService : INotificationProducer, IDisposable
{
    private readonly ILogger<ProducerService> _logger;
    private readonly ProducerConfig _config;
    private readonly IProducer<Null, MessageDto> _producer;

    public ProducerService(ILogger<ProducerService> logger, ProducerConfig config)
    {
        _logger = logger;
        _config = config;
        
        _producer = new ProducerBuilder<Null, MessageDto>(_config)
            .SetValueSerializer(new JsonSerializer<MessageDto>())
            .Build();
    }
    
    public async Task ProduceAsync(MessageDto message)
    {
        try
        {
            var deliveryResult = await _producer
                .ProduceAsync(topic: Topics.GetTopicForType(message.ChannelType),
                new Message<Null, MessageDto>
                {
                    Value = message,
                },CancellationToken.None);
            _logger.LogInformation($"Delivered message to {deliveryResult.Value}, Offset: {deliveryResult.Offset}");
        }
        catch (ProduceException<Null, MessageDto> e)
        {
            _logger.LogError($"Delivery failed: {e.Error.Reason}");
        }
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}