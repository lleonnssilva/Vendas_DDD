using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos;
using Vendas.Domain.Pedidos.Integration.Cliente;

namespace Vendas.Application.Commands.PedidosCommands.CriarPedido
{
    public sealed class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, CriarPedidoResultDto>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ClienteAcl _clienteAcl;
        private readonly IClienteGateway _clienteGateway;
        public CriarPedidoCommandHandler(IPedidoRepository pedidoRepository, ClienteAcl clienteAcl, IClienteGateway clienteGateway)
        {
            _pedidoRepository = pedidoRepository;
            _clienteAcl = clienteAcl;
            _clienteGateway = clienteGateway;
        }

        public async Task<CriarPedidoResultDto> HandleAsync(CriarPedidoCommand command, CancellationToken cancellationToken = default)
        {
            var dtoEndereco = await _clienteGateway.ObterEnderecoAsync(command.ClienteId, command.EnderecoId, cancellationToken = default);

            if (dtoEndereco == null)
                throw new DomainException("Endereço não localizado.");

            var enderecoEntrega = _clienteAcl.TraduzirEndereco(dtoEndereco);

            var pedido = Pedido.Criar(command.ClienteId, enderecoEntrega);

            await _pedidoRepository.AdicionarAsync(pedido, cancellationToken);

            return new CriarPedidoResultDto(
                pedido.Id,
                pedido.NumeroPedido,
                pedido.DataCriacao,
                pedido.ValorTotal,
                pedido.StatusPedido.ToString()
                );
        }
    }
}
