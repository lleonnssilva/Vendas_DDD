using Vendas.Domain.Pedidos.Enums;

namespace Vendas.Application.Queryes.Pedidos.ListarPedidosPagamentosPorStatus
{
    public sealed class ListarPedidosPagamentosPorStatusQuery
    {
        public ListarPedidosPagamentosPorStatusQuery(StatusPagamento status)
        {
            Status = status;
        }

        public StatusPagamento Status { get;}
    }
}
