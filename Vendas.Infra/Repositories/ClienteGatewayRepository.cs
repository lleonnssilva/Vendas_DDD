using Microsoft.EntityFrameworkCore;
using Vendas.Domain.Pedidos.Integration.Cliente;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{
    public class ClienteGatewayRepository : IClienteGateway
    {
        protected readonly AppDbContext _dbContext;

        public ClienteGatewayRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EnderecoDto?> ObterEnderecoAsync(Guid clienteId, Guid enderecoId, CancellationToken cancellationToken)
        {
            return  await _dbContext.Clientes
                .Where(p => p.Id == clienteId)
                .SelectMany(p => p.Enderecos)
                .Where(e => e.Id == enderecoId)
                .Select(e => new EnderecoDto(
                    e.Cep,
                    e.Logradouro,
                    e.Complemento,
                    e.Bairro,
                    e.Estado,
                    e.Cidade,
                    e.Pais,
                    e.Numero
                )).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }
    }
}
