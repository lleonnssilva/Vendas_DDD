using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AjustarEstoque
{
    public sealed class AjustarEstoqueCommand :IRequest<AjustarEstoqueResultDto>
    {
        public Guid ProdutoId { get; }
        public int Quantidade { get; }
        public string Motivo { get; }
        public AjustarEstoqueCommand(Guid produtoId, int quantidade, string motivo)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Motivo = motivo;
        }
    }
}
