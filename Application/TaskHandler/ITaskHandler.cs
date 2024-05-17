using BackgroundTasksService.AppServices.TaskHandler.Models;
using MassTransit;

namespace BackgroundTasksService.AppServices.TaskHandler
{
    public interface ITaskHandler
    {
        ValueTask<TaskResult> ExecuteAsync(CorrelatedBy<Guid> taskData, CancellationToken cancellationToken);
    }
}
