using System.Reflection;
using ChatGPTClone.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ChatGPTClone.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                //Validation Pipeline
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>)); // <,> : Generic Type/Placeholder
            });

            return services;
        }
    }
}
