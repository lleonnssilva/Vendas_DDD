namespace Vendas.Application.Commands.Pedidos.MarcarPedidoComoEntregue
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
