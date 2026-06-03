using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos;
using Vendas.Domain.Pedidos.Integration.Cliente;

namespace Vendas.Application.Commands.PedidosCommands.CriarPedido
{
    public sealed class CriarPedidoCommandHandler
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteGateway _clienteGateway;
        private readonly ClienteAcl _clienteAcl;
        public CriarPedidoCommandHandler(
            IPedidoRepository pedidoRepository, 
            IClienteGateway clienteGateway,
            ClienteAcl clienteAcl)
        {
            _pedidoRepository = pedidoRepository;
            _clienteGateway = clienteGateway;
            _clienteAcl = clienteAcl;
        }

        public async Task<CriarPedidoResultDto> HandleAsync(CriarPedidoCommand command, CancellationToken cancellationToken = default)
        {
            var enderecoDto = await _clienteGateway.ObterEnderecoAsync(command.ClienteId, command.EnderecoId, cancellationToken = default);

            if (enderecoDto == null)
                throw new DomainException("Endereço não localizado.");

            var enderecoEntrega = _clienteAcl.TraduzirEndereco(enderecoDto);

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
