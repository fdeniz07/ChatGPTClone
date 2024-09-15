using ChatGPTClone.Application.Common.Behaviours;
using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

                // Validation PipeLine
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>)); // <,> : Generic Type/Placeholder

            });

            return services;
        }
    }
}