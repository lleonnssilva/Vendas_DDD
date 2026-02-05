namespace Vendas.Application.Commands.Pedidos.AdicionarItemAoPedido
{
    public sealed class AdicionarItemAoPedidoResultDto
    {
        public Guid PedidoId { get;}
        public decimal ValorTotal { get; }
        public string Status { get; }

        public AdicionarItemAoPedidoResultDto(Guid pedidoId, decimal valorTotal, string status)
        {
            PedidoId = pedidoId;
            ValorTotal = valorTotal;
            Status = status;
        }
    }
}
