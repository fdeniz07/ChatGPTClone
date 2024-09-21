using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.WebApi.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace ChatGPTClone.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserManager>();

            services.AddSingleton<IEnvironmentService>(new EnvironmentManager(environment.WebRootPath));

            // Localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                // Set the default cultures
                var defaultCulture = new CultureInfo("tr-TR");


                // Set the supported cultures
                var supportedCultures = new List<CultureInfo>
                {
                    defaultCulture,
                    new CultureInfo("en-GB"),
                    new CultureInfo("de-DE")
                };

                // Add supported cultures
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);

                options.SupportedCultures = supportedCultures;

                options.SupportedUICultures = supportedCultures;

                options.ApplyCurrentCultureToResponseHeaders = true; // Dönen mesaji header'da da göster. Frontend tarafinda insanlarin hangi dil ile etkilesime gectigini ögrenebilirsiniz.
            });

            return services;
        }
    }
}
