using BackgroundTasksSerice.Worker;
using BackgroundTasksService.AppServices.Services;
using BackgroundTasksService.AppServices.StateMachines;
using BackgroundTasksService.DataAccess;
using BackgroundTasksService.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskWorker;
using BackgroundTasksService.AppServices.Services.Models;
using BackgroundTaskService.Infrastructure.Mapping;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddOptions<RabbitMqSettings>()
    .Configure<IConfiguration>((o, configuration) =>
    {
        configuration.GetSection(RabbitMqSettings.SectionName).Bind(o);
    });

builder.Services.AddOptions<TaskQueueSettings>()
    .Configure<IConfiguration>((o, configuration) =>
    {
        configuration.GetSection(TaskQueueSettings.SectionName).Bind(o);
    });

builder.Services.AddOptions<TaskHttpInvokerSettings>()
    .Configure<IConfiguration>((o, configuration) =>
    {
        configuration.GetSection(TaskHttpInvokerSettings.SectionName).Bind(o);
    });

builder.Services.AddMassTransit(cfg =>
    {
        cfg.UsingRabbitMq((ctx, rabbitCfg) =>
        {
            var rabbitMqSettings = ctx.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
            rabbitCfg.Host(rabbitMqSettings.Host, "/", h =>
            {
                h.Username(rabbitMqSettings.Login);
                h.Password(rabbitMqSettings.Password);
            });

            rabbitCfg.ReceiveEndpoint(typeof(TaskSagaStateMachine).Name, endpointCfg =>
            {
                endpointCfg.ConfigureSaga<TaskSaga>(ctx);
            });
        });

        cfg.AddSagaStateMachine<TaskSagaStateMachine, TaskSaga, TaskSagaDefinition>()
        .EntityFrameworkRepository(opt =>
        {
            opt.AddDbContext<DbContext, TaskSagaStateMachineDbContext>((context, dbContextOptionBuilder) =>
            {
                dbContextOptionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("TaskSagaDb"));
            });
            opt.ConcurrencyMode = ConcurrencyMode.Optimistic;
        });
    })
    .AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>()
    .AddScoped<IStateMachineActivity<TaskSaga, EnqueueTaskCommand>, EnqueueTaskActivity>()
    .AddScoped<IHttpTaskInvoker, HttpTaskInvoker>()
    .AddMapping()
    .AddHttpClient(HttpTaskInvoker.ClientName)
        .ConfigurePrimaryHttpMessageHandler((hmh, sp) =>
        {
#if DEBUG
            var httpClient = hmh as HttpClientHandler;
            httpClient.CheckCertificateRevocationList = false;
            httpClient.ServerCertificateCustomValidationCallback = (httpRequestMessage, x509Certificate2, x509Chain, sslPolicyErrors) => true;
#endif
        });

int workerCount = builder.Configuration.GetValue<int>(TaskQueueSettings.SectionName+ ":WorkerCount", 1);
for (var i = 0; i < workerCount; i++)
{
    builder.Services.AddSingleton<IHostedService, Worker>();
}

await builder.Build().RunAsync();