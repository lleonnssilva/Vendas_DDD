using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Pedidos;
using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Application.Commands.PedidosCommands.CriarPedido
{
    public sealed class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, CriarPedidoResultDto>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public CriarPedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<CriarPedidoResultDto> HandleAsync(
            CriarPedidoCommand command,
        CancellationToken cancellationToken = default
           )
        {
            var enderecoEntrega =  EnderecoEntrega.Criar(
                command.Cep, 
                command.Logradouro,
                command.Complemento,
                command.Bairro,
                command.Estado,
                command.Cidade,
                command.Pais,
                command.Numero);
            
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
