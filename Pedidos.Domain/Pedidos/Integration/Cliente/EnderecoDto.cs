namespace Vendas.Domain.Pedidos.Integration.Cliente
{
    public sealed class EnderecoDto
    {
        public string Rua { get;  }
        public string Numero { get; } 
        public string Complemento { get; }
        public string Bairro { get; } 
        public string Cidade { get; }
        public string Estado { get; } 
        public string Cep { get; } 
        public string Pais { get;  } 
        public EnderecoDto(string rua, string numero, string complemento, string bairro, string cidade, string estado, string cep, string pais)
        {
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            Pais = pais;
        }

       
    }
}
