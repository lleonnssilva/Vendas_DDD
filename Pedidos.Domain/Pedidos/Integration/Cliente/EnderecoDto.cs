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

        public EnderecoDto(
            Guid id, 
            string cep, 
            string logradouro,
            string numero,
            string bairro, 
            string cidade,
            string estado,
            string pais, 
            string complemento)
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
