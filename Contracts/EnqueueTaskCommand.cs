namespace Contracts
{
    using System;
    using MassTransit;

    public record EnqueueTaskCommand: CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; init; }
        public string TaskType { get; init; }
    }
}