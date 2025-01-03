using Confluent.Kafka;
using Service.Application;
using Service.Domain;
using Service.Notification.Core;


namespace Service.Notification.Gateway.Repeater;



internal class Repeater : BackgroundService
{
    private readonly ConsumerConfig _config;
    private readonly IConsumerResponseHandler _consumerResponseHandler;

    public Repeater(ConsumerConfig config, IConsumerResponseHandler consumerResponseHandler)
    {
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
