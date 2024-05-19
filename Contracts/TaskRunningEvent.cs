using MassTransit;

namespace BackgroundTasksService.Contracts
{
    public class TaskRunningEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
    }
}
