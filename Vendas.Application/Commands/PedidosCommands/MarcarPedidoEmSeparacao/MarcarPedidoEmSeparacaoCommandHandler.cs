using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoEmSeparacao
{
    public sealed class MarcarPedidoEmSeparacaoCommandHandler
    {
        private readonly IPedidoRepository _pedidoRepository;

        public MarcarPedidoEmSeparacaoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<MarcarPedidoEmSeparacaoResultDto> HandleAsync(MarcarPedidoEmSeparacaoCommand command, CancellationToken cancellationToken = default)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken)
                ?? throw new DomainException("Pedido não encontrado");

            pedido.MarcarComoEmSeparacao();
            await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);

            return new MarcarPedidoEmSeparacaoResultDto
            {
                PedidoId = pedido.Id,
                StatusPedido = pedido.StatusPedido.ToString()
            };
        }
    }
}