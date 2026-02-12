using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido
{
    public sealed class AdicionarItemAoPedidoCommand : IRequest<AdicionarItemAoPedidoResultDto>
    {
        public Guid PedidoId { get; }
        public Guid ProdutoId { get; }
        public string NomeProduto { get; }
        public decimal PrecoUnitario { get; }
        public int Quantidade { get; }

        public AdicionarItemAoPedidoCommand(
            Guid pedidoId, 
            Guid produtoId, 
            string nomeProduto, 
            decimal precoUnitario, 
            int quantidade)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
        }
    }
}
