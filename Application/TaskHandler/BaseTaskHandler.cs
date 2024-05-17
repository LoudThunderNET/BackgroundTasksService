using BackgroundTasksService.AppServices.TaskHandler.Models;
using MassTransit;

namespace BackgroundTasksService.AppServices.TaskHandler
{
    public abstract class BaseTaskHandler : ITaskHandler
    {
        /// <inheritdoc/>
        public abstract ValueTask<TaskResult> ExecuteAsync(CorrelatedBy<Guid> taskData, CancellationToken cancellationToken);
    }
}
