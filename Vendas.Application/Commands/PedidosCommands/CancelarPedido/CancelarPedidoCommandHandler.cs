using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Application.Commands.PedidosCommands.CancelarPedido
{
    public sealed class CancelarPedidoCommandHandler:IRequestHandler<CancelarPedidoCommand,CancelarPedidoResultDto>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public CancelarPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<CancelarPedidoResultDto> HandleAsync(CancelarPedidoCommand command, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId) ?? throw new DomainException("Pedido não localizado.");

            var motivo = new MotivoCancelamento(command.CodigoMotivo);

            pedido.CancelarPedido(motivo);

            await _pedidoRepository.AdicionarAsync(pedido, cancellationToken);


            return new CancelarPedidoResultDto
            {
                PedidoId = pedido.Id,
                Status = pedido.StatusPedido.ToString()
            };
        }
    }
}
