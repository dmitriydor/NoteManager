using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteManager.Api.Data;
using NoteManager.Api.Models;
using Npgsql;

namespace NoteManager.Api.Infrastructure.Extensions
{
    public static class DbExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(new NpgsqlConnection(configuration.GetSection("PostgreSql")
                    .GetValue<string>("ConnectionString"))));
            services.AddIdentityCore<User>().AddEntityFrameworkStores<AppDbContext>();
            return services;
        }
    }
}