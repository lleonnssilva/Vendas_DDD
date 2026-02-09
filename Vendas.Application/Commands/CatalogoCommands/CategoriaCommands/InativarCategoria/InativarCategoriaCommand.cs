namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.AtivarCategoria
{
    public sealed class InativarCategoriaCommand
    {
        public InativarCategoriaCommand(Guid categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public Guid CategoriaId { get; }
    }
}
