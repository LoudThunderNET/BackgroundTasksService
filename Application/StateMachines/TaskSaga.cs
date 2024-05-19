namespace BackgroundTasksService.AppServices.StateMachines
{
    using System;
    using MassTransit;

    public class TaskSaga : SagaStateMachineInstance 
    {
        public Guid CorrelationId { get; set; }

        public int CurrentState { get; set; }

        public string TaskType { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime? CompletedDateTime { get; set; }

        public string Payload { get; set; }
        public string ResultPayload { get; set; }
        public string Url { get; init; }
        public string HttpMethod { get; init; }
    }
}