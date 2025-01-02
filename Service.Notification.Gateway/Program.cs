using Confluent.Kafka;
using Service.Application.Gateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ProducerConfig>(_ => new ProducerConfig
{
    BootstrapServers = builder.Configuration.GetValue<string>("kafkaAddress"),
    AllowAutoCreateTopics = true,
    Acks = Acks.All,
    MessageSendMaxRetries = 5,
});

builder.Services.AddSingleton<INotificationProducer, ProducerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

