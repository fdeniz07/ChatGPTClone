using System.Reflection;
using FluentValidation;
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
            });

            return services;
        }
    }
}
