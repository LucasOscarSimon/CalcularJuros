using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TaxaJuros.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TAXA JUROS 1.0 - TAXA",
                    Description = "API TAXA JUROS 1.0 - TAXA"
                });
            });
        }
    }
}