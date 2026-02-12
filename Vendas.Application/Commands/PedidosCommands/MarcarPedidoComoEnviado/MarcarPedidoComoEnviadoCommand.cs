using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado
{
    public sealed class MarcarPedidoComoEnviadoCommand : IRequest<MarcarPedidoComoEnviadoResultDto>
    {
        public Guid PedidoId { get; }
        public MarcarPedidoComoEnviadoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}
