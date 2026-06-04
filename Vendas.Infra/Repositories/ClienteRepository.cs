using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Clientes;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{

    //public class ClienteRepository : IClienteRepository
    //{
    //    protected readonly VendasDbContext _dbContext;

    //    public ClienteRepository(VendasDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }
    //    public async Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    //    {
    //        await _dbContext.AddAsync(cliente);
    //        _dbContext.SaveChanges();
    //    }

    //    public async Task AtualizarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    //    {
    //        //_dbContext.Clientes.Update(cliente);
    //        //_dbContext.SaveChanges();
    //    }

    //    public async Task<Cliente?> ObterPorIdAsync(Guid clienteId, CancellationToken cancellationToken = default)
    //    {

    //        return null;
    //            //await _dbContext.Clientes.FindAsync(clienteId);
    //    }
    //}
}
