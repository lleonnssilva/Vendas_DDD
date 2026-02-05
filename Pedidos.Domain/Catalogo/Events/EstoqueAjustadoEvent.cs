using Vendas.Domain.Common.Base;

namespace Vendas.Domain.Catalogo.Events;

public sealed record class EstoqueAjustadoEvent(Guid ProdutoId, int Quantidade, string Motivo):DomainEventBase;
