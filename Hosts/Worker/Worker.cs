using BackgroundTasksService.AppServices.Services;

namespace TaskWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IServiceProvider _serviceProvider;

        public Worker(
            ILogger<Worker> logger,
            IBackgroundTaskQueue backgroundTaskQueue,
            IHostApplicationLifetime applicationLifetime,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _backgroundTaskQueue = backgroundTaskQueue;
            _applicationLifetime = applicationLifetime;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Ќачинаем разбор очереди задач.");
            while (!stoppingToken.IsCancellationRequested)
            {
                BackgroundTasksService.Contracts.EnqueueTaskCommand cmd = null;
                try
                {
                    cmd = await _backgroundTaskQueue.DequeueAsync(stoppingToken);
                    using var scope = _serviceProvider.CreateScope();
                    var httpTaskInvoker = scope.ServiceProvider.GetRequiredService<IHttpTaskInvoker>();

                    await httpTaskInvoker.StartAsync(cmd, stoppingToken);
                }
                catch (Exception e)
                {
                    if (_applicationLifetime.ApplicationStopping.IsCancellationRequested)
                    {
                        _logger.LogInformation("«авершаетс€ работа т.к. запрошено завершение приложени€.");
                    }
                    else
                    {
                        _logger.LogError(e, cmd == null
                            ? "Ќе удалось извлечь команду на запуск задачи из очереди"
                            : "«апуск обработки задачи '" + cmd.TaskType + "' завершилось ошибкой.");
                    }
                }
            }
        }
    }
}
