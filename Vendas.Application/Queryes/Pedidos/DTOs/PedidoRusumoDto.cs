namespace Vendas.Application.Queryes.Pedidos.DTOs
{
    public sealed class PedidoRusumoDto
    {
        public Guid Id { get; init; }
        public string NumeroPedido { get; init; } = string.Empty;
        public Guid ClienteId { get; init; }
        public Decimal ValorTotal { get; init; }
        public string StatusPedido { get; init; } = string.Empty;
        public DateTime DataCriacao { get; init; }
        public int TotalItens { get; init; }
        public int TotalPagamentos { get; init; }
    }
}
