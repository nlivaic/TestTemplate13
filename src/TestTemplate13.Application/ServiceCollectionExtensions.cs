using Microsoft.Extensions.DependencyInjection;
using TestTemplate13.Application.Pipelines;

namespace TestTemplate13.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTestTemplate13ApplicationHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions)));
            services.AddPipelines();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
