using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queryes.Pedidos.DTOs;

namespace Vendas.Application.Queryes.Pedidos.ListarPedidosPagamentosPorStatus
{
    public sealed class ListarPedidosPagamentosPorStatusQueryHandler
    {
        private readonly IPedidoQueryRespository _queryRepo;

        public ListarPedidosPagamentosPorStatusQueryHandler(IPedidoQueryRespository queryRepo)
        {
            _queryRepo = queryRepo;
        }

        public async Task<IReadOnlyList<PagamentoPorStatusDto>> HandleAsync(
            ListarPedidosPagamentosPorStatusQuery query,
            CancellationToken cancellationToken
            )
        {
            return await _queryRepo.ListarPagamentosPorStatusAsync(query.Status, cancellationToken);
        }
    }
}
