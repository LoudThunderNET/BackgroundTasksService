using System.Threading.Channels;
using BackgroundTasksService.AppServices.Services.Models;
using BackgroundTasksService.Contracts;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

namespace BackgroundTasksService.AppServices.Services
{
    public interface IBackgroundTaskQueue
    {
        ValueTask EnqueueAsync(EnqueueTaskCommand workItem, CancellationToken cancellationToken);

        ValueTask<EnqueueTaskCommand> DequeueAsync(CancellationToken cancellationToken);
    }

    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<EnqueueTaskCommand> _queue;

        public BackgroundTaskQueue(IOptions<TaskQueueSettings> options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            var channelOptions = new BoundedChannelOptions(options.Value.QueueCapacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<EnqueueTaskCommand>(channelOptions);
        }

        /// <inheritdoc/>
        public ValueTask EnqueueAsync(EnqueueTaskCommand workItem, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(workItem, nameof(workItem));

            return _queue.Writer.WriteAsync(workItem, cancellationToken);
        }

        /// <inheritdoc/>
        public ValueTask<EnqueueTaskCommand> DequeueAsync(CancellationToken cancellationToken)
        {
            return _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
