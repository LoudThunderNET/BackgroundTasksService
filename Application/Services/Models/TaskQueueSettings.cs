namespace BackgroundTasksService.AppServices.Services.Models
{
    public class TaskQueueSettings
    {
        public const string SectionName = "TaskQueueSettings";
        public int WorkerCount { get; set; } = 1;
        public int QueueCapacity { get; set; } = 180;
    }
}
