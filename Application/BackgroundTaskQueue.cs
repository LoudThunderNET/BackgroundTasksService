using System.Threading.Channels;
using BackgroundTasksService.AppServices.TaskHandler;

namespace MassTransit.StateMachine.Experiments
{
    public interface IBackgroundTaskQueue
    {
        ValueTask EnqueueAsync(ITaskHandler workItem, CancellationToken cancellationToken);

        ValueTask<ITaskHandler> DequeueAsync(CancellationToken cancellationToken);
    }

    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<ITaskHandler> _queue;

        public BackgroundTaskQueue(int capacity)
        {
            // Capacity should be set based on the expected application load and
            // number of concurrent threads accessing the queue.            
            // BoundedChannelFullMode.Wait will cause calls to WriteAsync() to return a task,
            // which completes only when space became available. This leads to backpressure,
            // in case too many publishers/calls start accumulating.
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<ITaskHandler>(options);
        }

        /// <inheritdoc/>
        public ValueTask EnqueueAsync(ITaskHandler workItem, CancellationToken cancellationToken)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            return _queue.Writer.WriteAsync(workItem, cancellationToken);
        }

        /// <inheritdoc/>
        public ValueTask<ITaskHandler> DequeueAsync(CancellationToken cancellationToken)
        {
            return _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
