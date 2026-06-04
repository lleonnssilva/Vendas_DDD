using Microsoft.EntityFrameworkCore;
using Vendas.Domain.Pedidos.Integration.Catalogo;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{
    //public class CatalogoGatewayRepository : ICatalogoGateway
    //{
        //protected readonly VendasDbContext _dbContext;

        //public CatalogoGatewayRepository(VendasDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        //public async Task<ProdutoDto?> ObterProdutoPorIdAsync(Guid produtoId, CancellationToken cancellationToken = default)
        //{
        //    return null;
        //     //   await _dbContext.Produtos
        //     //.Where(p => p.Id == produtoId)
        //     //.Select(p => new ProdutoDto(p.Id, p.Nome.Valor.ToString(), p.Preco.Valor))
        //     //.FirstOrDefaultAsync(cancellationToken);
        //}

        //public Task<bool> PossuiEstoqueDisponivelAsync(Guid produtoId, int quantidade, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}
    //}
}//
