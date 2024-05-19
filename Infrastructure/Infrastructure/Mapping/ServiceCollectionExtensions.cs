using BackgroundTasksService.AppServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundTaskService.Infrastructure.Mapping
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddSingleton<ITypeMapper, TypeMapper>();
            services.AddAutoMapper(configAction =>
            {
                configAction.AddProfile<TaskSagaProfile>();
            });

            return services;
        }
    }
}
