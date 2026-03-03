using Vendas.Domain.Common.Base;

namespace Vendas.Domain.Estoque.Events
{
    public record ProdutoCriadoEvent(Guid ProdutoId, string Nome) : DomainEventBase;
}
