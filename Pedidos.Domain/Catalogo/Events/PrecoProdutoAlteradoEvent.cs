using Vendas.Domain.Common.Base;

namespace Vendas.Domain.Catalogo.Events;

public sealed record class PrecoProdutoAlteradoEvent(Guid ProdutoId, string PrecoAntigo, string PrecoNovo) : DomainEventBase;

