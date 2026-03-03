using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Domain.Pedidos.Integration.Cliente
{
    public sealed class ClienteAcl
    {

        public EnderecoEntrega TraduzirEndereco(EnderecoDto dto)
        {
            return EnderecoEntrega.Criar(

                dto.Cep, 
                dto.Logradouro, 
                dto.Complemento, 
                dto.Bairro, 
                dto.Estado, 
                dto.Cidade, 
                dto.Pais, 
                dto.Numero
            );
        }

    }
}
