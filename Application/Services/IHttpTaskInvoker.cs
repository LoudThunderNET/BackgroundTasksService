using BackgroundTasksService.Contracts;

namespace BackgroundTasksService.AppServices.Services
{
    public interface IHttpTaskInvoker
    {
        ValueTask StartAsync(EnqueueTaskCommand enqueueTaskCommand, CancellationToken cancellationToken);
    }
}
