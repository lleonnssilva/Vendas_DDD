using Vendas.Domain.Estoque.Entities;

namespace Vendas.Application.Abstractions.Persistence
{
    public interface IEstoqueRepository
    {
        Task<Estoque?> ObterPorProdutoIdAsync(Guid produtoId, CancellationToken cancellationToken = default);
        Task<Estoque?> ObterPorIdAsync(Guid estoqueId, CancellationToken cancellationToken = default);
        Task AdicionarAsync(Estoque estoque, CancellationToken cancellationToken = default);
        Task AtualizarAsync(Estoque estoque, CancellationToken cancellationToken = default);
        Task<bool> PossuiEstoqueDisponivelAsync(Guid produtoId, int Quantidade, CancellationToken cancellationToken = default);
    }
}
