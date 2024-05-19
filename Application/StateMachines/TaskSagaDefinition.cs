namespace BackgroundTasksService.AppServices.StateMachines
{
    using MassTransit;

    public class TaskSagaDefinition :
        SagaDefinition<TaskSaga>
    {
        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, 
            ISagaConfigurator<TaskSaga> sagaConfigurator,
            IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
            endpointConfigurator.UseInMemoryOutbox(context);
            EndpointName = typeof(TaskSagaStateMachine).Name;
        }
    }
}