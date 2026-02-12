using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Application.Commands.ClientesCommands.CriarCliente
{

    public sealed class CriarClienteCommand : IRequest<CriarClienteResultDto>
    {
        public string Nome { get; }
        public string Cpf { get; }
        public string Email { get; }
        public string Telefone { get; }
        public string Cep { get; }
        public string Logradouro { get; }
        public string Numero { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Estado { get; }
        public string Pais { get; }
        public string Complemento { get; }
        public CriarClienteCommand(
            string nome,
            string cpf,
            string email,
            string telefone,
            string cep,
            string logradouro,
            string numero,
            string bairro,
            string cidade,
            string estado,
            string pais,
            string complemento)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Complemento = complemento;
        }

    }
}
