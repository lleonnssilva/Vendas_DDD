using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue
{
    public sealed class MarcarPedidoComoEntregueCommand : IRequest<MarcarPedidoComoEntregueResultDto>
    {
        public Guid PedidoId { get; }
        public MarcarPedidoComoEntregueCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}
