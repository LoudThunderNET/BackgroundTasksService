namespace Company.StateMachines
{
    using MassTransit;

    public class MassTransitStateMachineExperimentsStateSagaDefinition :
        SagaDefinition<BackGroundJobState>
    {
        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, 
            ISagaConfigurator<BackGroundJobState> sagaConfigurator,
            IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
            endpointConfigurator.UseInMemoryOutbox(context);
        }
    }
}