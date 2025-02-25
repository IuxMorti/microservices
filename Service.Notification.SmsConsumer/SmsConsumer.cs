using Confluent.Kafka;
using Service.Application;
using Service.Domain;
using Service.Notification.Core;

namespace Service.Notification.SmsConsumer;

internal class SmsConsumer : BackgroundService
{
    private readonly ILogger<SmsConsumer> _logger;
    private readonly ConsumerConfig _config;
    private readonly IConsumerResponseHandler _consumerResponseHandler;

    public SmsConsumer(ILogger<SmsConsumer> logger, ConsumerConfig config, IConsumerResponseHandler consumerResponseHandler)
    {
        _logger = logger;
        _config = config;
        _consumerResponseHandler = consumerResponseHandler;
    }
    
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, MessageDto>(_config)
            .SetValueDeserializer(new JsonDeserialize<MessageDto>())
            .Build();

        consumer.Subscribe(Topics.GetTopicForType("sms"));

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = consumer.Consume(TimeSpan.FromSeconds(5));

                if (consumeResult == null)
                {
                    continue;
                }

                _consumerResponseHandler.SendResponseAsync(new NotificationResponse()
                {
                    Status = NotificationStatus.OK,
                    CreationTime = consumeResult.Message.Timestamp.UtcDateTime,
                    DispatchTime = DateTime.UtcNow
                });
                
                _logger.LogInformation($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.Offset}'");
            }
            catch (Exception)
            {
                _consumerResponseHandler.SendResponseAsync(new NotificationResponse()
                {
                    Status = NotificationStatus.NoSend,
                    DispatchTime = DateTime.UtcNow
                });
            }
        }
        return Task.CompletedTask;
    }
}
