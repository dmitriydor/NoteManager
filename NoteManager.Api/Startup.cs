using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NoteManager.Api.Data;
using NoteManager.Api.Models;
using NoteManager.Api.Options;
using NoteManager.Api.Services;
using Npgsql;

namespace NoteManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        //todo: вынести в extentions все подключаемые сервисы 
        public void ConfigureServices(IServiceCollection services)
        {
            //registration custom services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            //jwt 
            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));
            services.Configure<SwaggerOptions>(Configuration.GetSection("SwaggerOptions"));
            var jwtOptions = new JwtOptions();
            Configuration.Bind(nameof(jwtOptions), jwtOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                });

            //sql
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(new NpgsqlConnection(Configuration.GetSection("PostgreSql").GetValue<string>("ConnectionString"))));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            
            // cors and controllers
            services.AddCors();
            services.AddControllers();
            
            //swagger
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = app.ApplicationServices.GetService<IOptions<SwaggerOptions>>().Value;
            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerOptions.Ui, swaggerOptions.Name));
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
