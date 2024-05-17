namespace Contracts
{
    using System;

    public record TaskCompletedEvent
    {
        public Guid CorrelationId { get; init; }
        public string TaskType { get; init; }
    }
}