using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queryes.Pedidos.DTOs;

namespace Vendas.Application.Queryes.Pedidos.ListarPedidosResumoPorCliente
{
    public sealed class ListarPedidosResumoPorClienteQueryHandler
    {
        private readonly IPedidoQueryRespository _queryRepo;


        public ListarPedidosResumoPorClienteQueryHandler(IPedidoQueryRespository queryRepo)
        {
            _queryRepo = queryRepo;
        }

        public async Task<IReadOnlyList<PedidoRusumoDto>> HandleAsync(
            ListarPedidosResumoPorClienteQuery query,
            CancellationToken cancellationToken = default)
        {
            return await _queryRepo.ListarResumoPorClienteAsync(query.ClienteId, cancellationToken);
        }
    }
}
