using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queryes.Pedidos.DTOs;

namespace Vendas.Application.Queryes.Pedidos.ListarPedidosResumo
{
    public sealed class ListarPedidosResumoQueryHandler
    {
        private readonly IPedidoQueryRespository _queryRepo;

        public ListarPedidosResumoQueryHandler(IPedidoQueryRespository queryRepo)
        {
            _queryRepo = queryRepo;
        }

        public async Task<IReadOnlyList<PedidoRusumoDto>> HandleAsync(
            ListarPedidosResumoQuery query,
            CancellationToken cancellation = default)
        => await _queryRepo.ListarResumoAsync(cancellation);

    }
}
