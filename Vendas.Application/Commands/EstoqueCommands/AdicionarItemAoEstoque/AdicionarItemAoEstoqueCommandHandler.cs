using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Estoque.Entities;

namespace Vendas.Application.Commands.EstoqueCommands.AdicionarItemAoEstoque
{
    public sealed class AdicionarItemAoEstoqueCommandHandler
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueRepository _estoqueRepository;
      
        public AdicionarItemAoEstoqueCommandHandler(IEstoqueRepository estoqueRepository, IProdutoRepository produtoRepository)
        {
            _estoqueRepository = estoqueRepository;
            _produtoRepository = produtoRepository;
        }


        public async Task<AdicionarItemAoEstoqueResultDto> HandleAsync(AdicionarItemAoEstoqueCommand command, CancellationToken cancellationToken = default)
        {
          
            var produto = await _produtoRepository.ObterPorIdAsync(command.ProdutoId, cancellationToken);
            if (produto is null)
                throw new DomainException("Produto não localizado.");

            var estoqueExiste = await _estoqueRepository.ObterPorProdutoIdAsync(command.ProdutoId, cancellationToken);
            if (estoqueExiste is not null)
                throw new DomainException("Produto já existe no estoque.");

            var estoque = new Estoque(
                command.ProdutoId,
                command.QuantidadeDisponivel,
                command.QuantidadeReservada
                );
          
            await _estoqueRepository.AdicionarAsync(estoque, cancellationToken);

            return new AdicionarItemAoEstoqueResultDto(
                command.ProdutoId,
                command.QuantidadeDisponivel,
                command.QuantidadeReservada
                );
        }
    }
}
