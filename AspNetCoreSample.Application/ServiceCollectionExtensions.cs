
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AspNetCoreSample.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(o => o.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
        //services.AddMediator(
        //    options =>
        //    {
        //        options.ServiceLifetime = ServiceLifetime.Scoped;
        //    }
        //);
        //return services
        //    .AddSingleton(typeof(IPipelineBehavior<,>), typeof(ErrorLoggingBehaviour<,>))
        //    .AddSingleton(typeof(IPipelineBehavior<,>), typeof(MessageValidatorBehaviour<,>));
    }
}
