using Confluent.Kafka;
namespace Service.Application.Gateway;
public class ProducerService : INotificationProducer
{
    private readonly ILogger<ProducerService> _logger;

    public ProducerService(ILogger<ProducerService> logger, ProducerConfig config)
    {
        _logger = logger;
    }

    public async Task ProduceAsync(MessageDto message)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            AllowAutoCreateTopics = true,
            Acks = Acks.All,
            MessageSendMaxRetries = 5,
        };
        
        using var producer = new ProducerBuilder<Guid, MessageDto>(config)
            .Build();
        try
        {
            var deliveryResult = await producer
                .ProduceAsync(topic: "test-topic",
                new Message<Guid, MessageDto>
                {
                    Key = message.BuyerId,
                    Value = message,
                },CancellationToken.None);
            //_logger.LogInformation($"Delivered message to {deliveryResult.Value}, Offset: {deliveryResult.Offset}");
        }
        catch (ProduceException<Null, string> e)
        {
            //_logger.LogError($"Delivery failed: {e.Error.Reason}");
        }
    }
}