using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.AtualizarEnderecoEntrega
{
    public sealed class AtualizarEnderecoEntregaCommandHandler
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AtualizarEnderecoEntregaCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<AtualizarEnderecoEntregaResultDto> HandleAsync(AtualizarEnderecoEntregaCommand command, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken);

            if (pedido is null)
                throw new InvalidOperationException("Pedido não localizado.");

            pedido.AtualizarEnderecoEntrega(
                command.NovoEnderecoEntrega
                );

            await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);

            return new AtualizarEnderecoEntregaResultDto(
                pedido.Id,
                pedido.EnderecoEntrega.ToString(),
                pedido.StatusPedido.ToString()
                );
        }
    }
}
