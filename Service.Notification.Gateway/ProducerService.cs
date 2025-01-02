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

    public async Task ProduceAsync(MessageDto message)
    {

        using var producer = new ProducerBuilder<Guid, MessageDto>(_config)
            .Build();
        try
        {
            var deliveryResult = await producer
                .ProduceAsync(topic: Topics.GetTopicForType(message.ChannelType),
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