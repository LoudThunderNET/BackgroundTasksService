using System.Runtime.Serialization;

namespace BackgroundTasksService.AppServices.Services
{
    [Serializable]
    public class HttpTaskInvokerException : Exception
    {
        public HttpTaskInvokerException()
        {
        }

        public HttpTaskInvokerException(string message) : base(message)
        {
        }

        public HttpTaskInvokerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpTaskInvokerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
