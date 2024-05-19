namespace BackgroundTasksService.AppServices.StateMachines
{
    using BackgroundTasksService.AppServices.Services;
    using BackgroundTasksService.Contracts;
    using MassTransit;

    public class TaskSagaStateMachine :
        MassTransitStateMachine<TaskSaga> 
    {
        public TaskSagaStateMachine()
        {
            InstanceState(backGroundJob => backGroundJob.CurrentState, /*2*/TaskEnqueued, /*3*/TaskRunning);

            Event(() => EnqueueTaskCommand, cc => cc.CorrelateById(context => context.Message.CorrelationId));
            Event(() => TaskRunningEvent, cc => cc.CorrelateById(context => context.Message.CorrelationId));
            Event(() => TaskCompeletedEvent, cc => cc. CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(EnqueueTaskCommand)
                .Activity(activitySelector => activitySelector.OfType<IStateMachineActivity<TaskSaga, EnqueueTaskCommand>>())
                .TransitionTo(TaskEnqueued)
            );

            During(TaskEnqueued,
                When(TaskRunningEvent)
                .TransitionTo(TaskRunning));

            During(TaskRunning,
                When(TaskCompeletedEvent)
                .Then(context => 
                {
                    context.Saga.ResultPayload = context.Message.Payload;
                    context.Saga.CompletedDateTime = context.SentTime;
                })
                .Finalize());

            SetCompletedWhenFinalized();
        }

        public State TaskEnqueued { get; private set; }
        public State TaskRunning { get; private set; }

        public Event<EnqueueTaskCommand> EnqueueTaskCommand { get; private set; }
        public Event<TaskRunningEvent> TaskRunningEvent { get; private set; }
        public Event<TaskCompletedEvent> TaskCompeletedEvent { get; private set; }
    }
}