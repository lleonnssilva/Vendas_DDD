using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Pedidos.Integration.Catalogo;
using Vendas.Domain.Common.Exceptions;
namespace Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido
{
    public sealed class AdicionarItemAoPedidoCommandHandler : IRequestHandler<AdicionarItemAoPedidoCommand, AdicionarItemAoPedidoResultDto>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CatalogoAcl _catalogoAcl;
        private readonly ICatalogoGateway _catalogoGateway;
        private readonly IEstoqueRepository _estoqueRepository;
        public AdicionarItemAoPedidoCommandHandler(IPedidoRepository pedidoRepository, CatalogoAcl catalogoAcl, ICatalogoGateway catalogoGateway, IEstoqueRepository estoqueRepository)
        {
            _pedidoRepository = pedidoRepository;
            _catalogoAcl = catalogoAcl;
            _catalogoGateway = catalogoGateway;
            _estoqueRepository = estoqueRepository;
        }


        public async Task<AdicionarItemAoPedidoResultDto> HandleAsync(AdicionarItemAoPedidoCommand command, CancellationToken cancellationToken = default)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken);
            if (pedido is null)
                throw new InvalidOperationException("Pedido não localizado.");

            var itemDto = await _catalogoGateway.ObterProdutoPorIdAsync(command.ProdutoId, cancellationToken);
            if (itemDto is null)
                throw new DomainException("Produto não localizado.");

            var disponivel = await _estoqueRepository.PossuiEstoqueDisponivelAsync(command.ProdutoId,command.Quantidade, cancellationToken);
            if (!disponivel)
                throw new DomainException("Estoque insulficiente para o produto.");


            var snapshot = _catalogoAcl.TraduzirItem(itemDto, command.Quantidade);

            pedido.AdicionarItem(snapshot);

            await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);

            return new AdicionarItemAoPedidoResultDto(
                pedido.Id,
                pedido.ValorTotal,
                pedido.StatusPedido.ToString()
                );
        }
    }
}
