using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.CancelarPedido
{
    public sealed class CancelarPedidoCommand
    {
        public Guid PedidoId { get; init; }
        public string CodigoMotivo { get; init; }

        public CancelarPedidoCommand(Guid pedidoId, string codigoMotivo)
        {
            PedidoId = pedidoId;
            CodigoMotivo = codigoMotivo;
        }

    }
}
