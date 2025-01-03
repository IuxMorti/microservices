using Confluent.Kafka;
using Service.Notification.Core;
using Service.Notification.SmsConsumer;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ConsumerConfig>(_ => new ConsumerConfig
{
    BootstrapServers = builder.Configuration.GetValue<string>("kafkaAddress"),
    GroupId = "sms-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
});

builder.Services.AddSingleton<ProducerConfig>(_ => 
    new ProducerConfig
    {
        BootstrapServers = builder.Configuration.GetValue<string>("kafkaAddress"),
        AllowAutoCreateTopics = true,
        Acks = Acks.All,
        MessageSendMaxRetries = 5,
    });
builder.Services.AddSingleton<IConsumerResponseHandler, ConsumerResponseHandler>();

builder.Services.AddHostedService<SmsConsumer>();

var host = builder.Build();
host.Run();