using System.Text.Json;
using Confluent.Kafka;
namespace Service.Application.Gateway;
public class ProducerService : INotificationProducer
{
    private readonly ILogger<ProducerService> _logger;
    private readonly ProducerConfig _config;

    public ProducerService(ILogger<ProducerService> logger, ProducerConfig config)
    {
        _logger = logger;
        _config = config;
    }

    public class JsonSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }
    }
    
    
    public async Task ProduceAsync(MessageDto message)
    {

        using var producer = new ProducerBuilder<Null, MessageDto>(_config)
            .SetValueSerializer(new JsonSerializer<MessageDto>())
            .Build();
        try
        {
            var deliveryResult = await producer
                .ProduceAsync(topic: Topics.GetTopicForType(message.ChannelType),
                new Message<Null, MessageDto>
                {
                    Value = message,
                },CancellationToken.None);
            //_logger.LogInformation($"Delivered message to {deliveryResult.Value}, Offset: {deliveryResult.Offset}");
        }
        catch (ProduceException<Null, MessageDto> e)
        {
            //_logger.LogError($"Delivery failed: {e.Error.Reason}");
        }
    }
}