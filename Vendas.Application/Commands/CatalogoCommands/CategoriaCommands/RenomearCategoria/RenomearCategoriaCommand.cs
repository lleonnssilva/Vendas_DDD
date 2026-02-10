namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.RenomearCategoria
{
    public sealed class RenomearCategoriaCommand
    {
        public Guid CategoriaId { get; }
        public string NovaCategoria { get; }
        public RenomearCategoriaCommand(
            Guid categoriaId,
            string novaCategoria)
        {
            CategoriaId = categoriaId;
            NovaCategoria = novaCategoria;
        }
        
    }
}
