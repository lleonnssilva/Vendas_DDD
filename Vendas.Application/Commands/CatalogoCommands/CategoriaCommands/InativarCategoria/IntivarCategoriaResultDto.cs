namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.AtivarCategoria
{
    public sealed class InativarCategoriaResultDto
    {
        public Guid CategoriaId { get; init; }
        public bool Inativa { get; init; }
    }
}
