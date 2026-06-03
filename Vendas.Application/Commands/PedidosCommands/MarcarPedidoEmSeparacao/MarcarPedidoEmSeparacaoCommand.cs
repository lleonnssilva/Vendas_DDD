namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoEmSeparacao
{
    public sealed class MarcarPedidoEmSeparacaoCommand
    {
        public Guid PedidoId { get; }
        public MarcarPedidoEmSeparacaoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}
