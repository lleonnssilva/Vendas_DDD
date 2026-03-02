using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Domain.Pedidos.Integration.Cliente
{
    public sealed class ClienteAcl
    {

        public EnderecoEntrega TraduzirEndereco(EnderecoDto dto)
        {

            return EnderecoEntrega.Criar(

                dto.Cep,
                dto.Numero,
                dto.Complemento,
                dto.Bairro,
                dto.Cidade,
                dto.Estado,
                dto.Rua,
                dto.Pais
            );
        }

    }
}
