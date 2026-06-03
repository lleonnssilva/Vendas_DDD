namespace Vendas.Domain.Pedidos.Integration.Cliente
{
    public interface IClienteGateway
    {
        Task<EnderecoDto?> ObterEnderecoAsync(
            Guid clienteId, 
            Guid enderecoId,
            CancellationToken cancellationToken = default);
    }
}
