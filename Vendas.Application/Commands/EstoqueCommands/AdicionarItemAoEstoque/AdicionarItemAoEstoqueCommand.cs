using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.EstoqueCommands.AdicionarItemAoEstoque
{
    public sealed class AdicionarItemAoEstoqueCommand : IRequest<AdicionarItemAoEstoqueResultDto>
    {
        public Guid ProdutoId { get; }
        public int QuantidadeDisponivel { get; }
        public int QuantidadeReservada { get; }
        public AdicionarItemAoEstoqueCommand(Guid produtoId, int quantidadeDisponivel, int quantidadeReservada)
        {
            ProdutoId = produtoId;
            QuantidadeDisponivel = quantidadeDisponivel;
            QuantidadeReservada = quantidadeReservada;
        }
        
    }
}
