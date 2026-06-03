using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoPago
{
    public sealed class MarcarPedidoComoPagoCommandHandler 
    {
        private readonly IPedidoRepository _pedidoRepository;

        public MarcarPedidoComoPagoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }



        public async Task<MarcarPedidoComoPagoResultDto> HandleAsync(MarcarPedidoComoPagoCommand command, CancellationToken cancellationToken = default)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken) ??
                throw new DomainException("Pedido não localizado.");

            var pagamento = pedido.Pagamentos.Any(x => x.PedidoId == pedido.Id && x.Id == command.PagamentoId).ToString() ??
               throw new DomainException("Pagamento não localizado.");


            pedido.HandlePagamentoAprovado(command.PagamentoId);

            await _pedidoRepository.AtualizarAsync(pedido);

            return new MarcarPedidoComoPagoResultDto
            {
                PedidoId = pedido.Id,
                StatusPedido = pedido.StatusPedido.ToString()
            };
        }
    }
}
