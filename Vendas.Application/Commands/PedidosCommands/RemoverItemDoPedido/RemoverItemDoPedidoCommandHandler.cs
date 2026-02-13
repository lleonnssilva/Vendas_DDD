using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.RemoverItemDoPedido
{
    public sealed class RemoverItemDoPedidoCommandHandler:IRequestHandler<RemoverItemDoPedidoCommand,RemoverItemDoPedidoResultDto>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public RemoverItemDoPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<RemoverItemDoPedidoResultDto> HandleAsync(RemoverItemDoPedidoCommand command, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken);

            if (pedido is null)
                throw new InvalidOperationException("Pedido não localizado");

            pedido.RemoverItem(command.ItemId);

            await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);

            return new RemoverItemDoPedidoResultDto(
                pedido.Id,
                pedido.ValorTotal,
                pedido.StatusPedido.ToString());
        }
    }
}
