
using AspNetCoreSample.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AspNetCoreSample.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(o => o.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
                                //.AddBehavior<IPipelineBehavior<AddTodoItemCommand, Result<TodoItem, ValidationFailed>>,ValidationBehaviour<AddTodoItemCommand, Result<TodoItem, ValidationFailed>>>()
                                );
        return services
            .AddSingleton<IValidator<AddTodoItemCommand>, AddTodoItemValidator>()
        //services.AddMediator(
        //    options =>
        //    {
        //        options.ServiceLifetime = ServiceLifetime.Scoped;
        //    }
        //);
        //return services
        //    .AddSingleton(typeof(IPipelineBehavior<,>), typeof(ErrorLoggingBehaviour<,>))
            //.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            .AddSingleton(typeof(IPipelineBehavior<AddTodoItemCommand, Result<TodoItem, ValidationFailed>>), typeof(ValidationBehaviour<AddTodoItemCommand, TodoItem>));
    }
}
