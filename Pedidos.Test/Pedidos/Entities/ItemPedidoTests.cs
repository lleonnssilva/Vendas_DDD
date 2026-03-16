using FluentAssertions;
using Vendas.Domain.Common.Base;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos;

namespace Vendas.Domain.Tests.Pedidos
{
    public class ItemPedidoTests
    {
        private static ItemPedido CriarItemValido(decimal preco = 100m, int quantidade = 2) {
            return new ItemPedido(Guid.NewGuid(), "Produto teste", preco, quantidade);
        }

        [Fact(DisplayName = "Deve criar itemPedido com sucesso quando dados válidos")]
        public void Criar_DeveRetornarItemPedido_QuandoDadosValidos()
        {
            var produtoId = Guid.NewGuid();
            var nomeProduto = "Teclado mecânico";
            var precoUnitario = 250m;
            var quantidade = 2;

            var item = new ItemPedido(produtoId, nomeProduto, precoUnitario, quantidade);

            item.ProdutoId.Should().Be(produtoId);
            item.NomeProduto.Should().Be(nomeProduto);
            item.PrecoUnitario.Should().Be(precoUnitario);
            item.Quantidade.Should().Be(quantidade);
            item.DescontoAplicado.Should().Be(0);
            item.ValorTotal.Should().Be(500m);
        }


        [Theory(DisplayName = "Deve lançar Exceção quando parâmetros inválidos")]
        [InlineData("", "Produto A", 10.0, 1, "ProdutoId inválido.")]
        [InlineData("guid", "", 10.0, 1, "O nome do produto é obrigatório.")]
        [InlineData("guid", "Produto B", 0, 1, "O preço unitário deve ser maior que zero.")]
        [InlineData("guid", "Produto C", 10.0, 0, "A quantidade deve ser maior que zero.")]
        public void Criar_DeveLancarExcecao_QuandoParametrosInvalidos(string tipo, string nomeProduto, decimal preco, int qtd, string mensagem)
        {
            //Arrange
            var produtoId = tipo == "guid" ? Guid.NewGuid() : Guid.Empty;

            //Act
            Action act = () => new ItemPedido(produtoId, nomeProduto, preco, qtd);

            //Assert
            act.Should().Throw<DomainException>()
                .WithMessage(mensagem);
        }

        [Fact(DisplayName = "Deve aplicar desconto com sucesso quando valor válido")]
        public void AplicarDesconto_DEveAplicarComSucesso_QuandoValorValido()
        {
            //Arrange
            var item = CriarItemValido(preco: 200m, quantidade: 2);

            //Act
            item.AplicarDesconto(50m);

            //Assert
            item.DescontoAplicado.Should().Be(50m);
            item.ValorTotal.Should().Be(350m);
            item.DataAtualizacao.Should().NotBeNull();
        }

        [Theory(DisplayName = "Deve lançar exceção ao applicar desconto inválido")]
        [InlineData(-10, "Desconto não pode ser negativo")]
        [InlineData(1000, "Desconto não pode exceder op valor total do item")]
        public void AplicarDesconto_DeveLancarExcecao_QuandoValorInvalido(decimal desconto, string mensagem)
        {
            //Arrange
            var item = CriarItemValido(preco: 100m, quantidade: 2);

            //Act
            Action act = () => item.AplicarDesconto(desconto);

            //Assert
            act.Should().Throw<DomainException>()
                .WithMessage(mensagem);
            //item.DataAtulizacao.Should().NotBeNull();
        }

        [Fact(DisplayName = "Deve adicionar unidades com sucesso quando valor válido")]
        public void AdicionarUnidades_DeveAdicionarUnidadesComSucesso_QuandoValorValido()
        {
            //Arrange
            var item = CriarItemValido(preco: 50m, quantidade: 2);

            //Act
            item.AdicionarUnidades(3);

            //Assert
            item.Quantidade.Should().Be(5);
            item.ValorTotal.Should().Be(250m);
            item.DataAtualizacao.Should().NotBeNull();
        }

        [Fact(DisplayName = "Deve adicionar exceção quando valor inválido")]
        public void AdicionarUnidades_DeveAdicionarExcecao_QuandoValorInvalido()
        {
            //Arrange
            var item = CriarItemValido();

            //Act
            Action act = () => item.AdicionarUnidades(0);

            //Assert
            act.Should()
                .Throw<DomainException>()
                .WithMessage("*pelo menos uma unidade*");
        }

        [Fact(DisplayName = "Deve remover unidade com sucesso quando valor válido")]
        public void RemoverUnidades_DeveRemoverComSucesso_QuandoValorValido()
        {
            //Arrange
            var item = CriarItemValido(preco: 100m, quantidade: 5);

            //Act
            item.RemoverUnidades(2);

            //Assert
            item.Quantidade.Should().Be(3);
            item.ValorTotal.Should().Be(300m);
            item.DataAtualizacao.Should().NotBeNull();
        }

        [Fact(DisplayName = "Deve lançar exceção ao remover unidades e zerar quantidade")]
        public void RemoverUnidades_DeveLancarExcecao_QuandoQuantidadeZerar()
        {
            //Arrange
            var item = CriarItemValido(preco: 100m, quantidade: 2);

            //Act
            Action act = () => item.RemoverUnidades(2);

            //Assert
            act.Should()
                .Throw<DomainException>()
                .WithMessage("*quantidade*Zero*");
        }

        //[Fact(DisplayName = "Deve atualizar preço unitario com sucesso quando valor válido")]
        //public void AtualizarPrecoUnitario_DeveLancarAtualizarComSucesso_QuandoValorValido()
        //{
        //    //Arrange
        //    var item = CriarItemValido(preco: 100m, quantidade: 3);

        //    //Act
        //    item.AtualizarPrecoUnitario(150m);

        //    //Assert
        //    item.PrecoUnitario.Should().Be(150m);
        //    item.ValorTotal.Should().Be(450m);
        //    item.DataAtualizacao.Should().NotBeNull();
        //}

        //[Fact(DisplayName = "Deve lançar exceção quando valor inválido")]
        //public void AtualizarPrecoUnitario_DeveLancarExcecao_QuandoValorInvalido()
        //{
        //    //Arrange
        //    var item = CriarItemValido();

        //    //Act
        //    Action act = () => item.AtualizarPrecoUnitario(0);

        //    //Assert
        //    act.Should().Throw<DomainException>()
        //         .WithMessage("*O preço unitário deve ser maior que zero*s");
        //    item.DataAtualizacao.Should().NotBeNull();
        //}

        [Fact(DisplayName = "Dois itens com mesmo Id devem ser considerados iguais")]
        public void Equals_DeveRetornarTrue_QuandoMesmoId()
        {
            //Arrange
            var item1 = CriarItemValido();
            var item2 = CriarItemValido();
            
            typeof(Entity).GetProperty("Id")!.SetValue(item1, item2.Id);

            //Assert
            (item1 == item2).Should().BeTrue();
            item1.Equals(item2).Should().BeTrue();
        }
    }
}
