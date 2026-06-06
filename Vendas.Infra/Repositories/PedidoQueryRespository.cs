
using Microsoft.EntityFrameworkCore;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queryes.Pedidos.DTOs;
using Vendas.Domain.Pedidos;
using Vendas.Domain.Pedidos.Enums;
using Vendas.Infra.Persistence.Context;

namespace Vendas.Infra.Repositories
{
    public sealed class PedidoQueryRespository : IPedidoQueryRespository
    {
        private readonly VendasDbContext _context;

        public PedidoQueryRespository(VendasDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<PagamentoPorStatusDto>> ListarPagamentosPorStatusAsync(StatusPagamento status, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Pagamento>().AsNoTracking()
               .Where(pg => pg.StatusPagamento == status)
               .Join(
                _context.Pedidos, pg => pg.PedidoId,
               p => p.Id, (pg, p) => new PagamentoPorStatusDto
               {
                   PagamentoId = p.Id,
                   NumeroPedido = p.NumeroPedido,
                   ClienteId = p.ClienteId,
                   ValorTotal = p.ValorTotal,
                   StatusPagamento = pg.StatusPagamento.ToString(),
                   MetodoPagamento = pg.MetodoPagamento.ToString(),
                   CodigoTransacao = pg.CodigoTransacao,
                   DataPagamento = pg.DataPagamento
               })
               .OrderBy(dto => dto.NumeroPedido)
               .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<PedidoRusumoDto>> ListarResumoAsync(CancellationToken cancellationToken = default)
        {

            return await _context.Pedidos
                .AsNoTracking()
                .Select(p => new PedidoRusumoDto
                {
                    Id = p.Id,
                    NumeroPedido = p.NumeroPedido,
                    ClienteId = p.ClienteId,
                    ValorTotal = p.ValorTotal,
                    StatusPedido = p.StatusPedido.ToString(),
                    DataCriacao = p.DataCriacao,
                    TotalItens = p.Itens.Count,
                    TotalPagamentos = p.Pagamentos.Count
                })
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<PedidoRusumoDto>> ListarResumoPorClienteAsync(Guid clienteId, CancellationToken cancellationToken = default)
        {
            return await _context.Pedidos.AsNoTracking()
                .Where(p => p.ClienteId == clienteId)
                .Select(p => new PedidoRusumoDto
                {
                    Id = p.Id,
                    NumeroPedido = p.NumeroPedido,
                    ClienteId = p.ClienteId,
                    ValorTotal = p.ValorTotal,
                    StatusPedido = p.StatusPedido.ToString(),
                    DataCriacao = p.DataCriacao,
                    TotalItens = p.Itens.Count,
                    TotalPagamentos = p.Pagamentos.Count
                })
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync(cancellationToken);
        }

        public async Task<PedidoCompletoDto?> ObterPedidoCompletoPorIdAsync(Guid pedidoId, CancellationToken cancellationToken = default)
        {
        

            return await _context.Pedidos.AsNoTracking()
               .Where(p => p.Id == pedidoId)
               .Select(p => new PedidoCompletoDto
               {
                   Id = p.Id,
                   NumeroPedido = p.NumeroPedido,
                   ClienteId = p.ClienteId,
                   ValorTotal = p.ValorTotal,
                   StatusPedido = p.StatusPedido.ToString(),
                   DataCriacao = p.DataCriacao,
                   DataAtualizacao = p.DataAtualizacao,

                   Endereco = new EnderecoEntregaDto
                   {
                       Cep = p.EnderecoEntrega.Cep,
                       Logradouro = p.EnderecoEntrega.Logradouro,
                       Numero = p.EnderecoEntrega.Numero,
                       Complemento = p.EnderecoEntrega.Complemento,
                       Bairro = p.EnderecoEntrega.Bairro,
                       Cidade = p.EnderecoEntrega.Cidade,
                       Estado = p.EnderecoEntrega.Estado,
                       Pais = p.EnderecoEntrega.Pais,
                   },

                   Itens = p.Itens.Select(i => new ItemResumoDto
                   {
                       ProdutoId = i.ProdutoId,
                       NomeProduto = i.NomeProduto,
                       PrecoUnitario = i.PrecoUnitario,
                       Quantidade = i.Quantidade,
                       ValorTotal = i.ValorTotal,

                   }).ToList(),

                   Pagamentos = p.Pagamentos.Select(pg => new PagamentoDto
                   {
                       PagamentoId = pg.Id,
                       MetodoPagamento = pg.MetodoPagamento.ToString(),
                       StatusPagamento = pg.StatusPagamento.ToString(),
                       Valor = pg.Valor,
                       CodigoTransacao = pg.CodigoTransacao,
                       DataPagamento = pg.DataPagamento
                   }).ToList()

               })
               .FirstOrDefaultAsync(cancellationToken);
        }

    }
}
