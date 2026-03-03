namespace Vendas.Domain.Pedidos.Integration.Catalogo
{
    public sealed class CatalogoAcl
    {
        public  ItemPedido TraduzirItem(ProdutoDto dto, int quantidade)
        {
            return  ItemPedido.Criar(
                dto.Id, 
                dto.Nome, 
                dto.Preco,
                quantidade
            );
        }
    }
}
