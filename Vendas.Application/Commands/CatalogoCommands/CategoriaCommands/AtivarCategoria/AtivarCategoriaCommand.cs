using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.AtivarCategoria
{
    public sealed class AtivarCategoriaCommand : IRequest<AtivarCategoriaResultDto>
    {
        public Guid CategoriaId { get; }
        public AtivarCategoriaCommand(Guid categoriaId)
        {
            CategoriaId = categoriaId;
        }
    }
}
