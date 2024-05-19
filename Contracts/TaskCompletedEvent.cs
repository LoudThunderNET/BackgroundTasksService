namespace BackgroundTasksService.Contracts
{
    using System;

    public record TaskCompletedEvent
    {
        public Guid CorrelationId { get; init; }
        public string TaskType { get; init; }
        public string Payload { get; init; }
    }
}