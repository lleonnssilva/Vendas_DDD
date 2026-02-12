namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoPago
{
    public sealed class MarcarPedidoComoPagoResultDto
    {
        public Guid PedidoId { get; init; }
        public string StatusPedido { get; init; } = string.Empty;
    }
}
