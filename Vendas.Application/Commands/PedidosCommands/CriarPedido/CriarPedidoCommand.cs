using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.PedidosCommands.CriarPedido
{
    public sealed class CriarPedidoCommand : IRequest<CriarPedidoResultDto>
    {
        public CriarPedidoCommand(Guid clienteId, Guid enderecoId)
        {
            ClienteId = clienteId;
            EnderecoId = enderecoId;
        }

        public Guid ClienteId { get; }
        public Guid EnderecoId { get; }
        
    }
}
