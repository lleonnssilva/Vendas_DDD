using Microsoft.EntityFrameworkCore;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Estoque.Entities;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        protected readonly AppDbContext _dbContext;

        public EstoqueRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AdicionarAsync(Estoque estoque, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(estoque);
            _dbContext.SaveChanges();
        }

        public async Task AtualizarAsync(Estoque estoque, CancellationToken cancellationToken = default)
        {
            //_dbContext.Estoques.Update(estoque);
            //_dbContext.SaveChanges();
        }

        public async Task<Estoque?> ObterPorIdAsync(Guid estoqueId, CancellationToken cancellationToken = default)
        {

            return null;
                //await _dbContext.Estoques.FindAsync(estoqueId);
        }

        public async Task<Estoque?> ObterPorProdutoIdAsync(Guid produtoId, CancellationToken cancellationToken = default)
        {
            return null;
               // await _dbContext.Estoques.Where(x => x.ProdutoId == produtoId).FirstOrDefaultAsync();
        }

        public async Task<bool> PossuiEstoqueDisponivelAsync(Guid produtoId, int quantidade, CancellationToken cancellationToken = default)
        {
            //var disponivel = await _dbContext.Estoques.Where(x => x.ProdutoId == produtoId && (x.QuantidadeReservada + quantidade) <= x.QuantidadeDisponivel).FirstOrDefaultAsync();
            return false; //disponivel is not null;
        }
    }
}
