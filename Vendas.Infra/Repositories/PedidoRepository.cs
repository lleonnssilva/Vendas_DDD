using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Pedidos;
using Vendas.Infra.Context;

namespace Vendas.Infra.Repositories
{
    internal class PedidoRepository : IPedidoRepository
    {
        protected readonly AppDbContext _dbContext;

        public PedidoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(pedido);
            _dbContext.SaveChanges();
        }

        public async Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default)
        {
             _dbContext.Pedidos.Update(pedido);
            _dbContext.SaveChanges();
        }

        public async Task<Pedido?> ObterPorIdAsync(Guid pedidoId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Pedidos.FindAsync(pedidoId);
        }
    }
}
