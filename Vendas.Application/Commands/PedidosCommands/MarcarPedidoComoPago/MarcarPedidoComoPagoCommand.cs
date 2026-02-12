using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoPago
{
    public sealed class MarcarPedidoComoPagoCommand : IRequest<MarcarPedidoComoPagoResultDto>
    {
        public Guid PedidoId { get; }
        public Guid PagamentoId { get; }
        public MarcarPedidoComoPagoCommand(Guid pedidoId, Guid pagamentoId)
        {
            PedidoId = pedidoId;
            PagamentoId = pagamentoId;
        }
    }
}
