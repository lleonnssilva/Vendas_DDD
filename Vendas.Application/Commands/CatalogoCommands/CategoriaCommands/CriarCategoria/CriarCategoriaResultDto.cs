namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.CriarCategoria
{
    public sealed class CriarCategoriaResultDto
    {
        public string Nome { get; init; }
        public string? Descricao { get; init; }
        public bool Ativa { get; init; }
    }
}
