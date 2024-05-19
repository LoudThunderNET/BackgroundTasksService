using BackgroundTasksService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<TaskSagaStateMachineDbContext>(optionsAction =>
        {
            optionsAction.UseSqlServer(hostContext.Configuration.GetConnectionString(TaskSagaStateMachineDbContext.ConnectionStringName));
            optionsAction.EnableSensitiveDataLogging();
        });
    })
.Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskSagaStateMachineDbContext>();
    await db.Database.MigrateAsync();
}