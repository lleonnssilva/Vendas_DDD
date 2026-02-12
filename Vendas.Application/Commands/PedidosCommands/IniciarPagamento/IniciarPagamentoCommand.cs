using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Pedidos.Enums;

namespace Vendas.Application.Commands.PedidosCommands.IniciarPagamento
{
    public sealed class IniciarPagamentoCommand : IRequest<IniciarPagamentoResultDto>
    {
        public Guid PedidoId { get;}
        public MetodoPagamento MetodoPagamento { get;}

        public IniciarPagamentoCommand(
            Guid pedidoId, 
            MetodoPagamento metodoPagamento)
        {
            PedidoId = pedidoId;
            MetodoPagamento = metodoPagamento;
        }

    }
}
