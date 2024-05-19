namespace BackgroundTasksService.Contracts
{
    using System;
    using MassTransit;

    public record EnqueueTaskCommand: CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; init; }
        public string TaskType { get; set; }
        public string Payload { get; init; }
        public string Url { get; init; }
        public string HttpMethod { get; init; }
    }
}