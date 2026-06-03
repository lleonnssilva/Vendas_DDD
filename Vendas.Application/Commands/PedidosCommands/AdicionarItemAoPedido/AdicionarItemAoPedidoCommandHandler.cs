using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos.Integration.Catalogo;
namespace Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido
{
    public sealed class AdicionarItemAoPedidoCommandHandler 
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICatalogoGateway _catalogoGateway;
        private readonly CatalogoAcl _catalogoAcl;
        public AdicionarItemAoPedidoCommandHandler(IPedidoRepository pedidoRepository, CatalogoAcl catalogoAcl, ICatalogoGateway catalogoGateway)
        {
            _pedidoRepository = pedidoRepository;
            _catalogoGateway = catalogoGateway;
            _catalogoAcl = catalogoAcl;
        }


        public async Task<AdicionarItemAoPedidoResultDto> HandleAsync(AdicionarItemAoPedidoCommand command, CancellationToken cancellationToken = default)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken);
            if (pedido is null)
                throw new InvalidOperationException("Pedido não localizado.");

            var itemDto = await _catalogoGateway.ObterProdutoPorIdAsync(command.ProdutoId, cancellationToken);
            if (itemDto is null)
                throw new DomainException("Produto não localizado.");

            var (nomeProduto, precoProduto) = _catalogoAcl.TraduzirProduto(itemDto);

            pedido.AdicionarItem(command.ProdutoId,nomeProduto,precoProduto,command.Quantidade);

            await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);

            return new AdicionarItemAoPedidoResultDto(
                pedido.Id,
                pedido.ValorTotal,
                pedido.StatusPedido.ToString()
                );
        }
    }
}
