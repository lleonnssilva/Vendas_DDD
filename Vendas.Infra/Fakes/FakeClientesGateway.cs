using Vendas.Domain.Pedidos.Integration.Cliente;

namespace Vendas.Infra.Fakes
{
    sealed class FakeClientesGateway : IClienteGateway
    {

        private static readonly Dictionary<Guid, Dictionary<Guid, EnderecoDto>> _clientes = new()
        {
            [new Guid("22222222-0000-0000-0000-000000000001")] = new()
            {
                [new Guid("33333333-0000-0000-0000-000000000001")] = new(
                    id: new Guid("33333333-0000-0000-0000-000000000001"),
                    cep: "01310-100", logradouro: "Avenida Paulista", numero: "1578", bairro: "Bela Vista", cidade: "São Paulo",
                    estado: "SP", pais: "Brasil", complemento: "Conj 42"
                 ),
            },
        };


        public Task<EnderecoDto?> ObterEnderecoAsync(
            Guid clienteId, 
            Guid enderecoId,
            CancellationToken cancellationToken)
        {
            if (_clientes.TryGetValue(clienteId, out var enderecos) && enderecos.TryGetValue(enderecoId, out var endereco))
            {
                return Task.FromResult<EnderecoDto?>(endereco);
            }
            return Task.FromResult<EnderecoDto?>(null);
        }
    }
}
