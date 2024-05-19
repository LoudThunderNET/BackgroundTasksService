using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTasksService.DataAccess
{   public class TaskSagaStateMachineDbContextFactory : IDesignTimeDbContextFactory<TaskSagaStateMachineDbContext>
    {
        public TaskSagaStateMachineDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskSagaStateMachineDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=BankEmulator;Trusted_Connection=True;Application Name=BankEmulator.Api");

            return new TaskSagaStateMachineDbContext(optionsBuilder.Options);
        }
    }
}
