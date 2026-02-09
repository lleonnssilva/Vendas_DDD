namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.CriarProduto
{
    public sealed class CriarProdutoResultDto
    {
        public Guid ProdutoId { get; init; }
        public string Nome { get; init; }
        public decimal Preco { get; init; }
        public string Status { get; init; }
    }
}
