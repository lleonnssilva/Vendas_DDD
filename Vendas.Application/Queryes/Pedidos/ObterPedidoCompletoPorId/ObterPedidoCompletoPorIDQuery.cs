namespace Vendas.Application.Queryes.Pedidos.ObterPedidoCompletoPorId
{
    public sealed class ObterPedidoCompletoPorIdQuery
    {
        public Guid PedidoId { get; }


        public ObterPedidoCompletoPorIdQuery(Guid pedidoId)
        {
            if (pedidoId == Guid.Empty)
                throw new ArgumentNullException("PedidoId inválido", nameof(pedidoId));
            PedidoId = pedidoId;
        }


    }
}
