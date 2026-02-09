namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue
{
    public sealed class MarcarPedidoComoEntregueCommand
    {
        public MarcarPedidoComoEntregueCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public Guid PedidoId { get; }
    }
}
