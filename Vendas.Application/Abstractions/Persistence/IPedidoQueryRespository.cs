using Vendas.Application.Queryes.Pedidos.DTOs;
using Vendas.Domain.Pedidos.Enums;

namespace Vendas.Application.Abstractions.Persistence
{
    public interface IPedidoQueryRespository
    {
       
        Task<IReadOnlyList<PedidoRusumoDto>> ListarResumoAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PedidoRusumoDto>> ListarResumoPorClienteAsync(Guid clienteId,CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PagamentoPorStatusDto>> ListarPagamentosPorStatusAsync(StatusPagamento status,CancellationToken cancellationToken = default);
        Task<PedidoCompletoDto?> ObterPedidoCompletoPorIdAsync(Guid pedidoId,CancellationToken cancellationToken = default);

    }
}
