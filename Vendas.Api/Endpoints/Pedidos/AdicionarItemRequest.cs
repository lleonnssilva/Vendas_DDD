namespace Vendas.Api.Endpoints.Pedidos
{
    public record AdicionarItemRequest(Guid ProdutoId, int Quantidade);
}
