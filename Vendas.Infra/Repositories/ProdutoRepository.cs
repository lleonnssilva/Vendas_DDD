using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Catalogo;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{

    public class ProdutoRepository : IProdutoRepository
    {
        protected readonly AppDbContext _dbContext;

        public ProdutoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(produto);
            _dbContext.SaveChanges();
        }

        public async Task AtualizarAsync(Produto produto, CancellationToken cancellationToken = default)
        {
            //_dbContext.Produtos.Update(produto);
            //_dbContext.SaveChanges();
        }

        public async Task<Produto?> ObterPorIdAsync(Guid produtoId, CancellationToken cancellationToken = default)
        {

            return null;//await _dbContext.Produtos.FindAsync(produtoId);
        }
    }
}
