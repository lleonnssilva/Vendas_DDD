namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AlterarCategoriaDoProduto
{
    public sealed class AlterarCategoriaDoProdutoCommand
    {
        public AlterarCategoriaDoProdutoCommand(
            Guid produtoId,
            Guid novaCategoriaId)
        {
            ProdutoId = produtoId;
            NovaCategoriaId = novaCategoriaId;
        }

        public Guid ProdutoId { get; }
        public Guid NovaCategoriaId { get; }
    }
}
