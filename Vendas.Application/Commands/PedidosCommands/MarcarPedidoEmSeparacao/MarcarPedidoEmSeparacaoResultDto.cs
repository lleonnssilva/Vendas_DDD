namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoEmSeparacao
{
    public sealed class MarcarPedidoEmSeparacaoResultDto
    {
        public Guid PedidoId { get; init; }
        public string StatusPedido { get; init; } = string.Empty;
    }
}
