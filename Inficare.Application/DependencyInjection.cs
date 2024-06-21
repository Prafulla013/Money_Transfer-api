using FluentValidation;
using Inficare.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inficare.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // Ref. https://code-maze.com/cqrs-mediatr-fluentvalidation/
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
