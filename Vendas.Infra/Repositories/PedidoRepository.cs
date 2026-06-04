

using Microsoft.EntityFrameworkCore;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Pedidos;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        protected readonly VendasDbContext _dbContext;
        private readonly SemaphoreSlim _lock = new(1, 1);
        public PedidoRepository(VendasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default)
        {

            //await _lock.WaitAsync(cancellationToken);
            //try
            //{
                await _dbContext.AddAsync(pedido,cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            //}
            //finally
            //{

            //    _lock.Release();
            //}
        }

        public async Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default)
        {

            await _lock.WaitAsync(cancellationToken);
            try
            {
               
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            finally
            {

                _lock.Release();
             }
        }

        public  async Task<IReadOnlyList<Pedido>> ListarTodosAsync(CancellationToken cancellationToken = default)
        {
            return  await _dbContext.Pedidos
                   .Include(p => p.Itens)
                   .Include(p => p.Pagamentos)
                   .AsSplitQuery()
                   .AsTracking()
                   .ToListAsync(cancellationToken);
        }

        public async Task<Pedido?> ObterPorIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try
            {
                return await _dbContext.Pedidos
                    .Include(p=> p.Itens)
                    .Include(p=> p.Pagamentos)
                    .FirstOrDefaultAsync(p=> p.Id == Id, cancellationToken);
            }
            finally
            {

                _lock.Release();
            }

        }
    }
}
