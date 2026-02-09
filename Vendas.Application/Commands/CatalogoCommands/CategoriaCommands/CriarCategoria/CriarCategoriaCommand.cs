namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.CriarCategoria
{
    public sealed class CriarCategoriaCommand
    {
        public CriarCategoriaCommand(string nome, string? descricao, bool ativa)
        {
            Nome = nome;
            Descricao = descricao;
            Ativa = ativa;
        }

        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public bool Ativa { get; private set; }
    }
}
