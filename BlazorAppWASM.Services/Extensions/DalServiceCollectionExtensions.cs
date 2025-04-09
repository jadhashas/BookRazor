using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppWASM.Services.Extensions
{
    public static class DalServiceCollectionExtensions
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
