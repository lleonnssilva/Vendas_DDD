namespace Vendas.Domain.Pedidos.Integration.Catalogo
{
    public sealed class CatalogoAcl
    {
        public (string nomeProduto, decimal precoUnitario) TraduzirItem(ProdutoDto dto)
        {
            return (dto.Nome, dto.Preco);
        }
    }
}
