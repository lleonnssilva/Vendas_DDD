using FluentAssertions;
using Vendas.Domain.Clientes.Entities;
using Vendas.Domain.Clientes.Enums;
using Vendas.Domain.Clientes.Events;
using Vendas.Domain.Clientes.ValueObjects;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Domain.Tests.Clientes.Entities
{
    public class ClienteTests
    {
        private static NomeCompleto CriarNomeCompleto(string nome = "João Silva") => new(nome);
        private static Cpf CriarCpf(string cpf = "25215344809") => new(cpf);
        private static Email CriarEmail(string email = "leoguaruleo@gmail.com") => new(email);
        private static Telefone CriarTelefone(string telefone = "11999999999") => new(telefone);
        private static Endereco CriarEndereco(
            string cep = "01310100",
            string logradoro = "Avenida Paulista",
            string numero = "1000",
            string bairro = "Bela Vista",
            string cidade = "São Paulo",
            string estado = "SP",
            string pais = "Brasil",
            string complemento = "") => new(cep, logradoro, numero, bairro, cidade, estado, pais, complemento);
        private static Cliente CriarClienteValido()
            => new Cliente(
            CriarNomeCompleto(),
            CriarCpf(),
            CriarEmail(),
            CriarTelefone(),
            CriarEndereco(),
            Sexo.Masculino,
            EstadoCivil.Solteiro);


        [Fact(DisplayName ="Construtor com dados válidos deve criar cliente")]
        public void Construtor_ComDadosValidos_DeveCriarCliente()
        {
            var cliente = CriarClienteValido();

            cliente.Status.Should().Be(StatusCliente.Ativo);
            cliente.Sexo.Should().Be(Sexo.Masculino);
            cliente.EstadoCivil.Should().Be(EstadoCivil.Solteiro);
            cliente.Enderecos.Should().ContainSingle();
            cliente.EnderecoPrincipalId.Should().Be(cliente.Enderecos.First().Id);
        }

        [Fact(DisplayName = "Construtor deve gerar evento cliente cadastrado")]
        public void Construtor_DeveGerarEventoClienteCadastrado()
        {
            var cliente = CriarClienteValido();

            cliente.Enderecos.Should().ContainSingle()
                .Which.Should().BeOfType<ClienteCadastradoEvent>(); 

        }

        [Theory(DisplayName ="Construtor com parâmetro obrigatorio nulos deve lançar Exception")]
        [InlineData("Nome")]
        [InlineData("Cpf")]
        [InlineData("Email")]
        [InlineData("Telefone")]
        [InlineData("Endereco")]
        public void Construtor_Com_Parametro_ObrigatorioNulo_DeveLancarDomainException(string campo)
        {
            NomeCompleto? nome = campo == "Nome" ? null : CriarNomeCompleto();
            Cpf? cpf = campo == "Cpf" ? null : CriarCpf();
            Email? email = campo == "Email" ? null : CriarEmail();
            Telefone? telefone = campo == "Telefone" ? null : CriarTelefone();
            Endereco? endereco = campo == "Endereco" ? null : CriarEndereco();

            Action act = () =>  new Cliente(nome!,cpf!,email!,telefone!,endereco!);

            act.Should().Throw<DomainException>();
        }
    }
}
