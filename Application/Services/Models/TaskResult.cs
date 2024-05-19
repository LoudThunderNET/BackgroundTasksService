namespace BackgroundTasksService.AppServices.Services.Models
{
    public struct TaskResult
    {
        private TaskResult(object result, bool isSuccess, IReadOnlyCollection<string> errors)
        {
            Result = result;
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public object Result { get; private set; }
        public bool IsSuccess { get; private set; }
        public IReadOnlyCollection<string> Errors { get; private set; }

        public static TaskResult Success(object result) => new TaskResult(result, false, Array.Empty<string>());
        public static TaskResult Fail(params string[] errorMessages) => new TaskResult(null, false, errorMessages);
    }
}
