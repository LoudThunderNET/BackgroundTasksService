using BackgroundTasksSerice.Worker;
using BackgroundTasksService.Contracts;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddOptions<RabbitMqSettings>()
    .Configure<IConfiguration>((o, configuration) =>
    {
        configuration.GetSection(RabbitMqSettings.SectionName).Bind(o);
    });
builder.Services.AddMassTransit(registrationCfg =>
    {
        registrationCfg.UsingRabbitMq((ctx, rabbitCfg) =>
        {
            var rabbitMqSettings = ctx.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
            rabbitCfg.Host(rabbitMqSettings.Host, "/", h =>
            {
                h.Username(rabbitMqSettings.Login);
                h.Password(rabbitMqSettings.Password);
            });
        });
    });

var host = builder.Build();
using (var scope = host.Services.CreateScope())
{
    var publisher = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
    await publisher.Publish(new TaskCompletedEvent()
    {
        CorrelationId = Guid.Parse("77BD31E7-B71A-418B-97F5-C64EF96CBD74"),
        //CorrelationId = Guid.NewGuid(),
        TaskType = "SampleTask",
        Payload = "Sample result",
        //Url = "http://localhost:1245"
    });
}
await host.RunAsync();