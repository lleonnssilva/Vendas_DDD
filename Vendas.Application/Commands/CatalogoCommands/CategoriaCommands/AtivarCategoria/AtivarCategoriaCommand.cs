namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.AtivarCategoria
{
    public sealed class AtivarCategoriaCommand
    {
        public AtivarCategoriaCommand(Guid categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public Guid CategoriaId { get; }
    }
}
