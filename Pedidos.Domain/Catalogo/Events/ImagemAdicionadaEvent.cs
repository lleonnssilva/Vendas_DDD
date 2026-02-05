using Vendas.Domain.Common.Base;

namespace Vendas.Domain.Catalogo.Events;

public sealed record class ImagemAdicionadaEvent(Guid ProdutoI, string Url, int Ordem):DomainEventBase;


