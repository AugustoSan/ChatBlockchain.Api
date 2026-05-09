using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ChatBlockchain.Core.Interfaces.Repositories;
using ChatBlockchain.Core.Interfaces.Services;
using ChatBlockchain.Infraestructure.Repositories;
using ChatBlockchain.Infraestructure.Data;
using ChatBlockchain.Infraestructure.Services;
using ChatBlockchain.Api.Services;

namespace ChatBlockchain.Infraestructure.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnectionString = configuration.GetSection("Connection:ConnectionString").Value;
            var DatabaseName = configuration.GetSection("Connection:DatabaseName").Value;

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(ConnectionString), ServiceLifetime.Scoped);

            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<INonceStore, MemoryNonceStore>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}