using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queryes.Pedidos.DTOs;

namespace Vendas.Application.Queryes.Pedidos.ObterPedidoCompletoPorId
{
    public sealed class ObterPedidoCompletoPorIdQueryHandler
    {
        private readonly IPedidoQueryRespository _queryRepo;


        public ObterPedidoCompletoPorIdQueryHandler(IPedidoQueryRespository queryRepo)
        {
            _queryRepo = queryRepo;
        }

        public async Task<PedidoCompletoDto?> HandleAsync(
            ObterPedidoCompletoPorIdQuery query, 
            CancellationToken cancellationToken = default)
        {
            var t =  await _queryRepo.ObterPedidoCompletoPorIdAsync(query.PedidoId, cancellationToken);
            var b = await _queryRepo.ObterPedidoCompletoPorIdAsync(query.PedidoId, cancellationToken);
            return await _queryRepo.ObterPedidoCompletoPorIdAsync(query.PedidoId, cancellationToken);
        }
    }
}
