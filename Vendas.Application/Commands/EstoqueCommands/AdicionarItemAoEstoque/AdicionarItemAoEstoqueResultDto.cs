namespace Vendas.Application.Commands.EstoqueCommands.AdicionarItemAoEstoque
{
    public sealed class AdicionarItemAoEstoqueResultDto
    {
        public Guid ProdutoId { get; }
        public int QuantidadeDisponivel { get; }
        public int QuantidadeReservada { get; }
        public AdicionarItemAoEstoqueResultDto(Guid produtoId, int quantidadeDisponivel, int quantidadeReservada)
        {
            ProdutoId = produtoId;
            QuantidadeDisponivel = quantidadeDisponivel;
            QuantidadeReservada = quantidadeReservada;
        }
    }
}
