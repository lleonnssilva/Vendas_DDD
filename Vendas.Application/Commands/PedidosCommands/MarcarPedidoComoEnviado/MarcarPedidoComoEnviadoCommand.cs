namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado
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
