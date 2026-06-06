using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Infra.Persistence.Context;
using Vendas.Infra.Repositories;

namespace Vendas.Infra.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<VendasDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueryRespository, PedidoQueryRespository>();
            return services;
        }
    }
}
