using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.AtivarCategoria
{
    public sealed class InativarCategoriaCommandHandler
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public InativarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<InativarCategoriaResultDto> HandleAsync(AtivarCategoriaCommand command, CancellationToken cancellationToken = default)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(command.CategoriaId, cancellationToken) ??
                throw new DomainException("Categoria não localizada.");

            categoria.Inativar();
            await _categoriaRepository.AtualizarAsync(categoria, cancellationToken);

            return new InativarCategoriaResultDto
            {
                CategoriaId = categoria.Id,
                Inativa = categoria.Ativa
            };
        }
    }
}
