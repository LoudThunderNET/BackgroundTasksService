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
            _logger.LogInformation("�������� ������ ������� �����.");
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
                        _logger.LogInformation("����������� ������ �.�. ��������� ���������� ����������.");
                    }
                    else
                    {
                        _logger.LogError(e, cmd == null
                            ? "�� ������� ������� ������� �� ������ ������ �� �������"
                            : "������ ��������� ������ '" + cmd.TaskType + "' ����������� �������.");
                    }
                }
            }
        }
    }
}
