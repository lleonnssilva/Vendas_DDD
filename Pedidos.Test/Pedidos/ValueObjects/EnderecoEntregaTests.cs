using FluentAssertions;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Domain.Tests.Pedidos.ValueObjects
{

    public class EnderecoEntregaTests
    {
        [Fact(DisplayName = "Deve criar EnderecoEntrega com suceasso quando todos os dados forem válidos")]
        public void Criar_DeveRetornarEnderecoValido_QuandoDadosForemValidos()
        {
            //Arrange
            var cep = "07140-230";
            var logradouro = "Avenida Otavio Braga de Mesquita";
            var complemento = "Apt 83";
            var bairro = "Jd São Geraldo";
            var estado = "SP";
            var cidade = "Guarulhos";
            var pais = "Brasil";
            var numero = "33";
            //Act
            var endereco = EnderecoEntrega.Criar(cep, logradouro, complemento, bairro, estado, cidade, pais,numero);


            //Assert
            endereco.Should().NotBeNull();
            endereco.Cep.Should().Be(cep);
            endereco.Logradouro.Should().Be(logradouro);
            endereco.Complemento.Should().Be(complemento);
            endereco.FormatarEndereco().Should().Contain("Avenida Otavio Braga de Mesquita");
        }

        [Theory(DisplayName = "Deve lançar Domain Excpetion quando o Cep for iválido")]
        [InlineData("123456789")]
        [InlineData("12-3456789")]
        [InlineData("ABCDE-123")]
        public void Criar_DeveLancarDomainException_QuandoCepForInValido(string cepInvalido)
        {
            //Arrange
            var logradouro = "Avenida Otavio Braga de Mesquita";
            var complemento = "Apt 83";
            var bairro = "Jd São Geraldo";
            var estado = "SP";
            var cidade = "Guarulhos";
            var pais = "Brasil";
            var numero = "33";
            //Act
           Action act =()=> EnderecoEntrega.Criar(cepInvalido, logradouro, complemento, bairro, estado, cidade, pais,numero);


            //Assert
            act.Should().Throw<DomainException>()
                .WithMessage("CEP inválido*");

        }

        [Fact(DisplayName = "Dois EnderecoEntrega com mesmos dados devem ser iguais (Value Object)")]
        public void EnderecosDevemSerIguais_QuandoPossuemMesmosValores()
        {
            //Arrange
            var endereco1 = EnderecoEntrega.Criar("07140-230", "Rua X", "Casa", "Centro", "SP", "Guarulhos", "Brasil", "33");
            var endereco2 = EnderecoEntrega.Criar("07140-230", "Rua X", "Casa", "Centro", "SP", "Guarulhos", "Brasil", "33");


            //Assert
            endereco1.Should().Be(endereco2);
            (endereco1 == endereco2).Should().BeTrue();
           
        }

        [Fact(DisplayName = "EnderecoEntrega devem ser diferentes quando algum campo for diferente")]
        public void EnderecosDevemSerDiferentes_QuandoAlgumCampoForDiferente()
        {
            //Arrange
            var endereco1 = EnderecoEntrega.Criar("07140-230", "Rua X", "Casa", "Centro", "SP", "Guarulhos", "Brasil", "33");
            var endereco2 = EnderecoEntrega.Criar("07140-230", "Rua Y", "Casa", "Centro", "SP", "Guarulhos", "Brasil", "33");


            //Assert
            endereco1.Should().NotBe(endereco2);


        }

        [Fact(DisplayName = "EnderecoEntrega deve ser imutável após a criação")]
        public void EnderecoDeveSerImutavel_AposCriacao()
        {
            //Arrange
            var endereco = EnderecoEntrega.Criar("07140-230", "Rua X", "Casa", "Centro", "SP", "Guarulhos", "Brasil", "33");

            //Act
            Action act = () =>
            {
                
            };

            //Assert
            endereco.GetType().GetProperties()
                .All(p => p.SetMethod == null || p.SetMethod.IsPrivate)
                .Should().BeTrue("as propriedades do VO devem ser imutáveis");


        }

        [Theory(DisplayName = "Deve lançar Domain Excpetion quando camppos obrigatórios forem nulos ou vazios")]
        [InlineData(null,"Logradouro","Bairro","Estado","Cidade","Pais","33")]
        [InlineData("07140-230", null, "Bairro", "Estado", "Cidade", "Pais","33")]
        [InlineData("07140-230", "Logradouro", "Bairro", "Estado", "Cidade", null,"33")]
        public void Criar_DeveLancarDomainException_QuandoCamposOrigatoriosulosOuVazios(string cep, string logradouro, string bairro, string estado, string cidade, string pais, string numero)
        {
         
            //Act
            Action act = () => EnderecoEntrega.Criar(cep, logradouro, "complemento", bairro, estado, cidade, pais, numero);


            //Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*não pode ser nulo ou vazio*");

        }
    }
}
