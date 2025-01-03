using Service.Notification.Gateway.Repeater;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Repeater>();

var host = builder.Build();
host.Run();