namespace Company.StateMachines
{
    using System;
    using MassTransit;

    public class BackGroundJobState :
        SagaStateMachineInstance 
    {
        public int CurrentState { get; set; }

        public string TaskType { get; set; }

        public Guid CorrelationId { get; set; }
    }
}