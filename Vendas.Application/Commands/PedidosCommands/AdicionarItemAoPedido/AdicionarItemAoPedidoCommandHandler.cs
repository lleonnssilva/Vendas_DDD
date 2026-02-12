using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido
{
    public sealed class AdicionarItemAoPedidoCommandHandler : IRequestHandler<AdicionarItemAoPedidoCommand, AdicionarItemAoPedidoResultDto>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AdicionarItemAoPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            this._pedidoRepository = pedidoRepository;
        }


        public async Task<AdicionarItemAoPedidoResultDto> HandleAsync(AdicionarItemAoPedidoCommand command, CancellationToken cancellationToken = default)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken);
            if (pedido is null)
                throw new InvalidOperationException("Pedido não localizado.");

            pedido.AdicionarItem(
                command.ProdutoId,
                command.NomeProduto,
                command.PrecoUnitario,
                command.Quantidade
                );

            await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);

            return new AdicionarItemAoPedidoResultDto(
                pedido.Id,
                pedido.ValorTotal,
                pedido.StatusPedido.ToString()
                );
        }
    }
}
