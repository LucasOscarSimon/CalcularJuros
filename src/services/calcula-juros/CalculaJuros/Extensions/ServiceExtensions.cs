using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CalculaJuros.Extensions
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
                    Title = "CALCULAR JUROS 1.0 - JUROS",
                    Description = "API CALCULAR JUROS 1.0 - JUROS",
                    Contact = new OpenApiContact
                    {
                        Name = "CALCULAR JUROS 1.0 - JUROS",
                        Email = string.Empty,
                    }
                });

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        },
                //        new string[] { }
                //    }
                //});
            });
        }

        public static void ConfigureDockerIpAddress(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DockerGatewayIpAddressConfiguration>(configuration.GetSection("DockerGatewayIpAddress"));
        }

        public static void ConfigureHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient();
        }
    }
}