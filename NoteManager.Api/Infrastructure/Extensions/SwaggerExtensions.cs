using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NoteManager.Api.Options;

namespace NoteManager.Api.Infrastructure.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("note_manager", new OpenApiInfo {Title = "Note Manager API", Version = "v1"});
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme", 
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey, 
                    In = ParameterLocation.Header
                });
                options.AddSecurityRequirement( new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            
            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            var swaggerOptions = app.ApplicationServices.GetService<IOptions<SwaggerOptions>>().Value;
            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerOptions.Ui, swaggerOptions.Name));

            return app;
        }
    }
}