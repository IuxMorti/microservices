using Confluent.Kafka;
using Service.Notification.SmsConsumer;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ConsumerConfig>(_ => new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "sms-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
});
builder.Services.AddHostedService<SmsConsumer>();



var host = builder.Build();
host.Run();