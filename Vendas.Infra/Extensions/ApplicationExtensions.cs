using Microsoft.Extensions.DependencyInjection;
using Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido;
using Vendas.Application.Commands.PedidosCommands.CancelarPedido;
using Vendas.Application.Commands.PedidosCommands.CriarPedido;
using Vendas.Application.Commands.PedidosCommands.IniciarPagamento;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoEmSeparacao;
using Vendas.Application.Queryes.Pedidos.ListarPedidosPagamentosPorStatus;
using Vendas.Application.Queryes.Pedidos.ListarPedidosResumo;
using Vendas.Application.Queryes.Pedidos.ListarPedidosResumoPorCliente;
using Vendas.Application.Queryes.Pedidos.ObterPedidoCompletoPorId;

namespace Vendas.Infra.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ListarPedidosPagamentosPorStatusQueryHandler>();
            services.AddScoped<ListarPedidosResumoQueryHandler>();
            services.AddScoped<ListarPedidosResumoPorClienteQueryHandler>();
            services.AddScoped<ObterPedidoCompletoPorIdQueryHandler>();

            services.AddScoped<CriarPedidoCommandHandler>();
            services.AddScoped<AdicionarItemAoPedidoCommandHandler>();
            services.AddScoped<IniciarPagamentoCommandHandler>();
            services.AddScoped<MarcarPedidoComoEnviadoCommandHandler>();
            services.AddScoped<MarcarPedidoComoEntregueCommandHandler>();
            services.AddScoped<CancelarPedidoCommandHandler>();
            services.AddScoped<MarcarPedidoEmSeparacaoCommandHandler>();



           



            return services;
        }
    }
  
}
