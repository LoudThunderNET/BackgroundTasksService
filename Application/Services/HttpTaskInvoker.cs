using BackgroundTasksService.AppServices.Extensions;
using BackgroundTasksService.AppServices.Services.Models;
using BackgroundTasksService.Contracts;
using MassTransit;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text;

namespace BackgroundTasksService.AppServices.Services
{
    public class HttpTaskInvoker : IHttpTaskInvoker
    {
        public const string ClientName = "HttpTaskInvoker";

        private readonly HttpClient _httpClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public HttpTaskInvoker(IHttpClientFactory httpClientFactory, 
            IOptions<TaskHttpInvokerSettings> options,
            IPublishEndpoint publishEndpoint)
        {
            _httpClient = httpClientFactory.CreateClient(ClientName);
            _httpClient.Timeout = options.Value.Timeout;
            _publishEndpoint = publishEndpoint;
        }

        public async ValueTask StartAsync(EnqueueTaskCommand enqueueTaskCommand, CancellationToken cancellationToken)
        {
            //await _publishEndpoint.Publish(new TaskRunningEvent()
            //{
            //    CorrelationId = enqueueTaskCommand.CorrelationId,
            //}, cancellationToken);
            //return;

            using var httpMessageRequest = new HttpRequestMessage(HttpMethod.Parse(enqueueTaskCommand.HttpMethod), enqueueTaskCommand.Url);
            if (enqueueTaskCommand.Payload.IsNotNullOrEmpty())
            {
                httpMessageRequest.Content = new StringContent(enqueueTaskCommand.Payload, Encoding.UTF8, MediaTypeNames.Application.Json);
            }
            using var httpMessageResponse = await _httpClient.SendAsync(httpMessageRequest, cancellationToken);
            if (!httpMessageResponse.IsSuccessStatusCode)
            {
                ThrowHttpTaskInvokerException();
                return;
            }

            await _publishEndpoint.Publish(new TaskRunningEvent()
            {
                CorrelationId = enqueueTaskCommand.CorrelationId,
            }, cancellationToken);

            void ThrowHttpTaskInvokerException()
            {
                var e = new HttpTaskInvokerException();
                e.Data[nameof(enqueueTaskCommand.TaskType)] = enqueueTaskCommand.TaskType;
                e.Data[nameof(enqueueTaskCommand.Url)] = enqueueTaskCommand.Url;
                e.Data[nameof(enqueueTaskCommand.HttpMethod)] = enqueueTaskCommand.HttpMethod;
                e.Data[nameof(enqueueTaskCommand.Payload)] = enqueueTaskCommand.Payload;

                throw e;
            }
        }
    }
}
