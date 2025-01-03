using System.Text.Json;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Service.Application;
using Service.Application.Gateway;

namespace Service.Notification.SmsConsumer;

internal class SmsConsumer : BackgroundService
{
    private readonly ILogger<SmsConsumer> _logger;
    private readonly ConsumerConfig _config;

    public SmsConsumer(ILogger<SmsConsumer> logger, ConsumerConfig config)
    {
        _logger = logger;
        _config = config;
    }

    public class JsonDeserialize<T>  : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
             return JsonSerializer.Deserialize<T>(data)!;
        }
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

                _logger.LogInformation($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.Offset}'");
            }
            catch (Exception)
            {
                // Ignore
            }
        }
        return Task.CompletedTask;
    }
    
    
    
    
    
}
