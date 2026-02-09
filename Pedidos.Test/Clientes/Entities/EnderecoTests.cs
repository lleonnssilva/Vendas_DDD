using FluentAssertions;
using Vendas.Domain.Clientes;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Domain.Tests.Clientes
{
    public class EnderecoTests
    {
        private static Endereco CriarEnderecoValido()
        {
            return new Endereco(
                cep: "12345678",
                logradouro: "Rua A",
                numero: "100",
                bairro: "Centro",
                cidade: "Guarulhos",
                estado: "SP",
                pais: "Brasil"
                );
        }

        [Fact(DisplayName = "Deve criar endereço válido")]
        public void Dev_Criar_Endereco_Valido()
        {
            var endereco = CriarEnderecoValido();

            endereco.Cep.Should().Be("12345678");
            endereco.Logradouro.Should().Be("Rua A"); endereco.Cep.Should().Be("12345678");
            endereco.Numero.Should().Be("100");
            endereco.Bairro.Should().Be("Centro");
            endereco.Cidade.Should().Be("Guarulhos");
            endereco.Estado.Should().Be("SP");
            endereco.Pais.Should().Be("Brasil");
            endereco.Complemento.Should().BeEmpty();

        }

        [Theory(DisplayName = "Deve lançar erro quando cep for inválido")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Deve_Lancar_Erro_Quando_CEP_For_Invalido(string? cepInvalido)
        {
            Action act = () => new Endereco(
                cep: cepInvalido!,
                logradouro: "Rua A",
                numero: "100",
                bairro: "Centro",
                cidade: "Guarulhos",
                estado: "SP",
                pais: "Brasil"
                );

            act.Should().Throw<DomainException>()
            .WithMessage("O CEP é obrigatório.");
        }

        [Fact(DisplayName = "Deve lançar erro quando cep não tiover 8 digitos")]
        public void Dev_Lancar_Erro_Quando_CEP_Nao_Tiver_8_Digitos()
        {
            Action act = () => new Endereco(
                 cep: "0123",
                 logradouro: "Rua A",
                 numero: "100",
                 bairro: "Centro",
                 cidade: "Guarulhos",
                 estado: "SP",
                 pais: "Brasil"
                 );
            act.Should().Throw<DomainException>()
            .WithMessage("O CEP inválido.");
        }

        [Theory(DisplayName = "Deve lançar erro quando campos obrigatórios forem invalidos")]
        [InlineData(null, "100", "Centro", "São Paulo", "SP", "Brasil")]
        [InlineData("Rua A", null, "Centro", "São Paulo", "SP", "Brasil")]
        [InlineData("Rua A", "100", null, "São Paulo", "SP", "Brasil")]
        [InlineData("Rua A", "100", "Centro", null, "SP", "Brasil")]
        [InlineData("Rua A", "100", "Centro", "São Paulo", null, "Brasil")]
        [InlineData("Rua A", "100", "Centro", "São Paulo", "SP", null)]
        public void Deve_Lancar_Erro_Quando_Campos_Obrigatorios_Forem_Invalido(
            string? logradouro,
            string? numero,
            string? bairro,
            string? cidade,
            string? estado,
            string? pais

            )
        {
            Action act = () => new Endereco(
                cep: "12345678",
                logradouro: logradouro!,
                numero: numero!,
                bairro: bairro!,
                cidade: cidade!,
                estado: estado!,
                pais: pais!
                );

            act.Should().Throw<DomainException>();

        }

        [Fact(DisplayName = "Deve atualizar endereço com dados válidos")]
        public void Deve_Atualizar_Endereco_Com_Dados_Validos()
        {
            var endereco = CriarEnderecoValido();
            endereco.Atualizar(
                 cep: "01234567",
                 logradouro: "Rua A",
                 numero: "100",
                 bairro: "Centro",
                 cidade: "Guarulhos",
                 estado: "SP",
                 pais: "Brasil",
                 complemento: "Apt 83"
                 );
            endereco.Cep.Should().Be("01234567");
            endereco.Logradouro.Should().Be("Rua A");
            endereco.Numero.Should().Be("100");
            endereco.Bairro.Should().Be("Centro");
            endereco.Cidade.Should().Be("Guarulhos");
            endereco.Estado.Should().Be("SP");
            endereco.Pais.Should().Be("Brasil");
            endereco.Complemento.Should().Be("Apt 83");
        }

        [Fact(DisplayName = "Deve lançar erro ao atualizar com cep inválido")]
        public void Deve_Lancar_Erro_Ao_Atualizar_Com_CEP_Invalido()
        {
            var endereco = CriarEnderecoValido();
            Action act = () => endereco.Atualizar(
                 cep: "012",
                 logradouro: "Rua A",
                 numero: "100",
                 bairro: "Centro",
                 cidade: "Guarulhos",
                 estado: "SP",
                 pais: "Brasil",
                 complemento: "Apt 83"
                 );
            act.Should().Throw<DomainException>();
        }

        [Fact(DisplayName = "Deve lançar erro ao atualizar com campo origatorio invalido")]
        public void Deve_Lancar_Erro_Ao_Atualizar_Com_Campo_Obrigatorio_Invalido()
        {
            var endereco = CriarEnderecoValido();
            Action act = () => endereco.Atualizar(
                 cep: "12345678",
                 logradouro: "",
                 numero: "100",
                 bairro: "Centro",
                 cidade: "Guarulhos",
                 estado: "SP",
                 pais: "Brasil"
                 );
            act.Should().Throw<DomainException>();
        }
    }
}
