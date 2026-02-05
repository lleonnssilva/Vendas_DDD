using Vendas.Domain.Common.Base;

namespace Vendas.Domain.Catalogo.Events
{
    public sealed record class ProdutoAtivadoEvent(Guid ProdutoId):DomainEventBase;
}
