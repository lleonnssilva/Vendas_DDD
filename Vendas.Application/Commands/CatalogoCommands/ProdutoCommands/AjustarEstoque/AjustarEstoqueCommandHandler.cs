using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AjustarEstoque
{
    public sealed class AjustarEstoqueCommandHandler : IRequestHandler<AjustarEstoqueCommand, AjustarEstoqueResultDto>
    {
        private readonly IProdutoRepository _produtoRepository;

        public AjustarEstoqueCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<AjustarEstoqueResultDto> HandleAsync(AjustarEstoqueCommand command, CancellationToken cancellationToken = default)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(command.ProdutoId, cancellationToken) ??
                throw new DomainException("Produto não localizado.");

            produto.AjustarEstoque(command.Quantidade, command.Motivo);
            await _produtoRepository.AtualizarAsync(produto, cancellationToken);

            return new AjustarEstoqueResultDto
            {
                ProdutoId = produto.Id,
                EstoqueAtual = command.Quantidade,
            };
        }
    }
}
