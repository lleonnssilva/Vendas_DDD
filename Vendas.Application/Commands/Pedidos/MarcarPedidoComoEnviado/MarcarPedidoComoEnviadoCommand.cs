namespace Vendas.Application.Commands.Pedidos.MarcarPedidoComoEnviado
{
    public sealed class MarcarPedidoComoEnviadoCommand
    {
        public MarcarPedidoComoEnviadoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public Guid PedidoId { get; }
    }
}
