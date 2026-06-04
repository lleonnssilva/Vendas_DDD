using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Catalogo;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{
    //public class CategoriaRepository : ICategoriaRepository
    //{
    //    protected readonly VendasDbContext _dbContext;

    //    public CategoriaRepository(VendasDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }
    //    public async Task AdicionarAsync(Categoria categoria, CancellationToken cancellationToken = default)
    //    {
    //        await _dbContext.AddAsync(categoria);
    //        _dbContext.SaveChanges();
    //    }

    //    public async Task AtualizarAsync(Categoria categoria, CancellationToken cancellationToken = default)
    //    {
    //        //_dbContext.Categorias.Update(categoria);
    //        //_dbContext.SaveChanges();
    //    }

    //    public async Task<Categoria?> ObterPorIdAsync(Guid categoriaId, CancellationToken cancellationToken = default)
    //    {
    //        return null;
    //            //await _dbContext.Categorias.FindAsync(categoriaId);
    //    }
    //}
}
