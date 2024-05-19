namespace BackgroundTasksService.AppServices.Extensions
{
    public static class StringExensions
    {
        public static bool IsNullOrEmpty(this string probeString) => string.IsNullOrEmpty(probeString);
        public static bool IsNotNullOrEmpty(this string probeString) => !probeString.IsNullOrEmpty();
    }
}
