using Vendas.Domain.Common.Base;

namespace Vendas.Domain.Estoque.Events
{
    public record EstoqueCriadoEvent(Guid ProdutoId, string Nome) : DomainEventBase;
}
