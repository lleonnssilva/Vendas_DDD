namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.RenomearCategoria
{
    public sealed class RenomearCategoriaCommand
    {
        public RenomearCategoriaCommand(
            Guid categoriaId,
            string novaCategoria)
        {
            CategoriaId = categoriaId;
            NovaCategoria = novaCategoria;
        }

        public Guid CategoriaId { get; }
        public string NovaCategoria { get; }
    }
}
