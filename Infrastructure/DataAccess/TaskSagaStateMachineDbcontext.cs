using BackgroundTasksService.DataAccess.Configuration;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BackgroundTasksService.DataAccess
{
    public class TaskSagaStateMachineDbContext : SagaDbContext
    {
        public const string ConnectionStringName = "TaskSagaDb";
        public TaskSagaStateMachineDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get 
            {
                yield return new TaskSagaStateConfiguration();
            }
        }
    }
}
