namespace Vendas.Api.Endpoints.Pedidos
{
    public record CriarPedidoRequest(Guid ClienteId, Guid EnderecoId);
}
