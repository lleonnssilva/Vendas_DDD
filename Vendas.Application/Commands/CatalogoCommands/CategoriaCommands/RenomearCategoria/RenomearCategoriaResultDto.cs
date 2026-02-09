namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.RenomearCategoria
{
    public sealed class RenomearCategoriaResultDto
    {
        public Guid CategoriaId { get; init; }
        public string NovaCategoria  { get; init; }
    }
}
