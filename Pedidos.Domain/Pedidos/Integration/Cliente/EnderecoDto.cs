namespace Vendas.Domain.Pedidos.Integration.Cliente
{
    public sealed class EnderecoDto
    {
        public string Cep { get; }
        public string Logradouro { get; }
        public string Complemento { get; }
        public string Bairro { get; }
        public string Estado { get; }
        public string Cidade { get; }
        public string Pais { get; }
        public string Numero { get; }

        public EnderecoDto(string cep, string logradouro, string complemento, string bairro, string estado, string cidade, string pais, string numero)
        {
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Estado = estado;
            Cidade = cidade;
            Pais = pais;
            Numero = numero;
        }


    }
}
