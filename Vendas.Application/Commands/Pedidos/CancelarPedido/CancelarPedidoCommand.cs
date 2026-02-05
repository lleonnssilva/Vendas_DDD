namespace Vendas.Application.Commands.Pedidos.CancelarPedido
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
