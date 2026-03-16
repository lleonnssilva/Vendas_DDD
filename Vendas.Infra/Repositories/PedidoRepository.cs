using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Pedidos;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        protected readonly AppDbContext _dbContext;
        private readonly SemaphoreSlim _lock = new(1, 1);
        public PedidoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default)
        {

            await _lock.WaitAsync(cancellationToken);
            try
            {
                await _dbContext.AddAsync(pedido);
                _dbContext.SaveChanges();
            }
            finally
            {

                _lock.Release();
            }
        }

        public async Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default)
        {

            await _lock.WaitAsync(cancellationToken);
            try
            {
                _dbContext.Pedidos.Update(pedido);
                _dbContext.SaveChanges();
            }
            finally
            {

                _lock.Release();
            }
        }

        public async Task<Pedido?> ObterPorIdAsync(Guid pedidoId, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try
            {
                return await _dbContext.Pedidos.FindAsync(pedidoId);
            }
            finally
            {

                _lock.Release();
            }

        }
    }
}
