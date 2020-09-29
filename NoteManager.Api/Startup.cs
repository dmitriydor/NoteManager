using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoteManager.Api.Data.Repositories;
using NoteManager.Api.Infrastructure.Extensions;

namespace NoteManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //registration custom services
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            
            //jwt 
            services.AddJwtAuthentication(Configuration);
            
            //database
            services.AddDatabaseContext(Configuration);
            
            //cors and controllers
            services.AddCors();
            services.AddControllers();
            
            //swagger
            services.AddSwagger(Configuration);
            
            //automapper
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
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
