namespace Company.StateMachines
{
    using Contracts;
    using MassTransit;

    public class MassTransitStateMachineExperimentsStateMachine :
        MassTransitStateMachine<BackGroundJobState> 
    {
        public MassTransitStateMachineExperimentsStateMachine()
        {
            InstanceState(backGroundJob => backGroundJob.CurrentState, Created);

            Event(() => MassTransitStateMachineExperimentsEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(MassTransitStateMachineExperimentsEvent)
                    .Then(context => context.Saga.TaskType = context.Message.TaskType)
                    .TransitionTo(Created)
            );

            SetCompletedWhenFinalized();
        }

        public State Created { get; private set; }

        public Event<EnqueueTaskCommand> MassTransitStateMachineExperimentsEvent { get; private set; }
    }
}