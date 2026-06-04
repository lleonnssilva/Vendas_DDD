using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vendas.Domain.Pedidos.Integration.Catalogo;
using Vendas.Domain.Pedidos.Integration.Cliente;
using Vendas.Infra.Fakes;

namespace Vendas.Infra.Extensions
{
    public static class IntegrationExtensions
    {
        public static IServiceCollection AddIntegration(this IServiceCollection services)
        {

            services.AddSingleton<ICatalogoGateway,FakeCatalogoGateway>();
            services.AddSingleton<IClienteGateway, FakeClientesGateway>();
            
            services.AddSingleton<CatalogoAcl>();
            services.AddSingleton<ClienteAcl>();

            return services;
        }
    }
}
