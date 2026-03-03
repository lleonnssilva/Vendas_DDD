using Microsoft.EntityFrameworkCore;
using Vendas.Domain.Pedidos.Integration.Catalogo;
using Vendas.Infra.Context;

namespace Vendas.Infra.Repositories
{
    public class CatalogoGatewayRepository : ICatalogoGateway
    {
        protected readonly AppDbContext _dbContext;

        public CatalogoGatewayRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ProdutoDto?> ObterProdutoPorIdAsync(Guid produtoId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Produtos
             .Where(p => p.Id == produtoId)
             .Select(p => new ProdutoDto(p.Id, p.Nome.Valor.ToString(), p.Preco.Valor, true, 0))
             .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
