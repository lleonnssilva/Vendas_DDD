using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.AtivarCategoria
{
    public sealed class InativarCategoriaCommand
    {
        public Guid CategoriaId { get; }
        public InativarCategoriaCommand(Guid categoriaId)
        {
            CategoriaId = categoriaId;
        }

    }
}
