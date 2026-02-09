using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Application.Commands.PedidosCommands.CancelarPedido
{
    public sealed class CancelarPedidoCommandHandler
    {
        private readonly IPedidoRepository _pedidoRepository;

        public CancelarPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<CancelarPedidoResultDto> HandlerAsync(CancelarPedidoCommand command, CancellationToken cancellationToken = default)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId) ?? throw new DomainException("Pedido não encontrado.");

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
