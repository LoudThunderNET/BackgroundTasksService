using BackgroundTasksService.AppServices.StateMachines;
using BackgroundTasksService.Contracts;
using MassTransit;

namespace BackgroundTasksService.AppServices.Services
{
    public class EnqueueTaskActivity : IStateMachineActivity<TaskSaga, EnqueueTaskCommand>
    {
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly ITypeMapper _typeMapper;

        public EnqueueTaskActivity(IBackgroundTaskQueue backgroundTaskQueue, ITypeMapper typeMapper)
        {
            _backgroundTaskQueue = backgroundTaskQueue;
            _typeMapper = typeMapper;
        }

        /// <inheritdoc/>
        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <inheritdoc/>
        public async Task Execute(BehaviorContext<TaskSaga, EnqueueTaskCommand> context, IBehavior<TaskSaga, EnqueueTaskCommand> next)
        {
            var enqueueTaskCommand = context.Message;
            _typeMapper.Map(enqueueTaskCommand, context.Saga);

            await _backgroundTaskQueue.EnqueueAsync(enqueueTaskCommand, context.CancellationToken);

            await next.Execute(context);
        }

        /// <inheritdoc/>
        public Task Faulted<TException>(
            BehaviorExceptionContext<TaskSaga, EnqueueTaskCommand, TException> context,
            IBehavior<TaskSaga, EnqueueTaskCommand> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        /// <inheritdoc/>
        public void Probe(ProbeContext context)
        {
            context.CreateScope("task-enqueued");
        }
    }
}
