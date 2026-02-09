using FluentAssertions;
using System.Reflection;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos;
using Vendas.Domain.Pedidos.Enums;
using Vendas.Domain.Pedidos.Events;
using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Domain.Tests.Pedidos
{
    public class PedidoTests
    {
        private static EnderecoEntrega CriarEnderecoValido()
            => EnderecoEntrega.Criar("12345-000", "Rua A", "Ap 12", "Centro", "SP", "Guarulhos", "Brasil");

        private static readonly Guid ClientIdValido = Guid.NewGuid();
        private static readonly Guid ProdutoIdValido = Guid.NewGuid();

        private static void SetStatusPedido(Pedido pedido, StatusPedido status)
        {
            typeof(Pedido).GetProperty(nameof(Pedido.StatusPedido),
                BindingFlags.Public | BindingFlags.Instance)!
                .SetValue(pedido, status);

        }

        [Fact(DisplayName = "Deve criar pedido válido com status Pendente")]
        public void Deve_CriarPedido_Valido()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());

            pedido.Should().NotBeNull();
            pedido.ClienteId.Should().Be(ClientIdValido);
            pedido.EnderecoEntrega.Should().NotBeNull();
            pedido.StatusPedido.Should().Be(StatusPedido.Pendente);
            pedido.ValorTotal.Should().Be(0);
            pedido.Itens.Should().BeEmpty();
            pedido.Pagamentos.Should().BeEmpty();
            pedido.Id.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Não deve criar pedido com ClienteId inválido")]
        public void Nao_Deve_Criar_Pedido_Com_ClienteId_Invalido()
        {
            Action act = () => Pedido.Criar(Guid.Empty, CriarEnderecoValido());

            act.Should().Throw<DomainException>()
                .WithMessage("ClienteId inválido");
        }

        [Fact(DisplayName = "Não deve criar pedido sem endereo de entrega")]
        public void Nao_Deve_Criar_Pedido_Sem_Endereco()
        {
            Action act = () => Pedido.Criar(ClientIdValido, null!);

            act.Should().Throw<DomainException>()
                .WithMessage("O endereço de entrega é obrigatório.");
        }

        [Fact(DisplayName = "Deve adicionar item ao pedido")]
        public void Deve_Adicionar_Item_Ao_Pedido()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());

            pedido.AdicionarItem(ProdutoIdValido, "Mouse", 100m, 2);

            pedido.Itens.Should().HaveCount(1);
            pedido.ValorTotal.Should().Be(200m);
            pedido.Itens.First().ValorTotal.Should().Be(200m);
        }

        [Fact(DisplayName = "Deve somar quantidade de item existente ao adicionar produto")]
        public void Deve_Somar_Quantidade_De_Item_Existente()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            var produtoId = ProdutoIdValido;

            pedido.AdicionarItem(produtoId, "Teclado", 200m, 1);
            pedido.AdicionarItem(produtoId, "Teclado", 200m, 2);

            pedido.Itens.Should().HaveCount(1);
            var item = pedido.Itens.First();
            item.Quantidade.Should().Be(3);
            item.ValorTotal.Should().Be(600m);
            pedido.ValorTotal.Should().Be(600m);
        }

        [Theory(DisplayName = "Não deve permitir adicionar itens quando pedido estiver Pendente")]
        [InlineData(StatusPedido.PagamentoConfirmado)]
        [InlineData(StatusPedido.EmSeparacao)]
        [InlineData(StatusPedido.Enviado)]
        [InlineData(StatusPedido.Entregue)]
        [InlineData(StatusPedido.Cancelado)]
        public void Não_Deve_Adicionar_Item_Quando_Pedido_Nao_Estiver_Pendente(StatusPedido statusPedido)
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            SetStatusPedido(pedido, statusPedido);

            Action act = () => pedido.AdicionarItem(Guid.NewGuid(), "Outro", 100m, 1);
            act.Should().Throw<DomainException>()
                .WithMessage("Itens só podem ser adicionados enquanto o pedido está pendente."); ;
        }

        [Fact(DisplayName = "Deve remover item e recalcular valor total")]
        public void Deve_Remover_Item_E_Recalcular_Valor()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());

            pedido.AdicionarItem(ProdutoIdValido, "Teclado", 100m, 2);

            Action act = () => pedido.RemoverItem(pedido.Itens.First().Id);

            act.Should().Throw<DomainException>()
                .WithMessage("O pedido deve conter pelo menos um item.");
        }

        [Fact(DisplayName = "Deve remover item e recalcular valor total quando houver mais de um item")]
        public void Deve_Remover_Item_Quando_Houver_Mais_De_Um()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            var produto1 = Guid.NewGuid();
            var produto2 = Guid.NewGuid();


            pedido.AdicionarItem(produto1, "Teclado", 100m, 1);
            pedido.AdicionarItem(produto2, "Mouse", 200m, 1);
            var itemId = pedido.Itens.First(i => i.ProdutoId == produto1).Id;
            pedido.RemoverItem(itemId);

            pedido.Itens.Should().HaveCount(1);
            pedido.ValorTotal.Should().Be(200m);

        }

        [Fact(DisplayName = "Deve ignorar a remoção de item inexistente")]
        public void Deve_Ignorar_Remocao_De_Item_Inexistente()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());

            pedido.AdicionarItem(ProdutoIdValido, "Teclado", 100m, 2);

            Action act = () => pedido.RemoverItem(Guid.NewGuid());

            act.Should().Throw<DomainException>()
                .WithMessage("Item não encontrado no pedido.");

        }

        [Theory(DisplayName = "Não deve permitir remover itens quando pedido não estiver Pendente")]
        [InlineData(StatusPedido.PagamentoConfirmado)]
        [InlineData(StatusPedido.EmSeparacao)]
        [InlineData(StatusPedido.Enviado)]
        [InlineData(StatusPedido.Entregue)]
        [InlineData(StatusPedido.Cancelado)]
        public void Não_Deve_Remover_Item_Quando_Nao_Pendente(StatusPedido statusPedido)
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 10m, 1);
            SetStatusPedido(pedido, statusPedido);

            Action act = () => pedido.RemoverItem(ProdutoIdValido);
            act.Should().Throw<DomainException>()
                .WithMessage("Itens só podem ser removidos em pedido pendente.");
        }

        [Fact(DisplayName = "Deve atualizar endereço de entrega quando Pendente")]
        public void Deve_Atualizar_Endereco_Quando_Pendente()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());

            var novoEndereco = EnderecoEntrega.Criar("00000-000", "Rua nova", "Casa", "airro Novo", "SP", "Guarulhos", "Brasil");

            pedido.AtualizarEnderecoEntrega(novoEndereco);

            pedido.EnderecoEntrega.Should().Be(novoEndereco);
        }

        [Theory(DisplayName = "Não deve atualizar endereço de enrtega quando não Pendente")]
        [InlineData(StatusPedido.PagamentoConfirmado)]
        [InlineData(StatusPedido.EmSeparacao)]
        [InlineData(StatusPedido.Enviado)]
        [InlineData(StatusPedido.Entregue)]
        [InlineData(StatusPedido.Cancelado)]
        public void Não_Deve_Atualizar_Endereco_Quando_Nao_Pendente(StatusPedido statusPedido)
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 10m, 1);
            var novoEndereco = EnderecoEntrega.Criar("00000-000", "Rua nova", "Casa", "airro Novo", "SP", "Guarulhos", "Brasil");
            SetStatusPedido(pedido, statusPedido);

            Action act = () => pedido.AtualizarEnderecoEntrega(novoEndereco);

            act.Should().Throw<DomainException>()
                .WithMessage("O endereço só pode ser alterado enquanto o pedido está pendente.");
        }
        /// <summary>
        /// Pagamentos
        /// </summary>

        [Fact(DisplayName = "Deve iniciar pagamento e manter status Pendente")]
        public void Deve_Iniciar_Pagamento()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 100m, 2);

            var pagamento = pedido.IniciarPagamento(MetodoPagamento.CartaoCredito);

            pagamento.Should().NotBeNull();
            pagamento.Valor.Should().Be(200m);
            pedido.Pagamentos.Should().Contain(pagamento);
            pedido.StatusPedido.Should().Be(StatusPedido.Pendente);

        }

        [Fact(DisplayName = "Não deve iniciar pagamento sem itens do pedido")]
        public void Nao_Deve_Iniciar_Pagamento_Sem_Items()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());

            Action act = () => pedido.IniciarPagamento(MetodoPagamento.Pix);

            act.Should().Throw<DomainException>()
                .WithMessage("Não é possível iniciar o pagamento de um pedido sem itens.");
        }

        [Fact(DisplayName = "Não deve iniciar pagamento se houver pagamento pendente")]
        public void Nao_Deve_Iniciar_Pagamento_Se_Houver_Pendente()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 100m, 1);


            Action act = () => pedido.IniciarPagamento(MetodoPagamento.CartaoCredito);

           act.Should().Throw<DomainException>()
               .WithMessage("Já existe um pagamento para o pedido.");
        }

        [Fact(DisplayName = "Deve alterar status para PagamentoConfirmado ao HandlePagamentoAprovado")]
        public void Deve_Alterar_Status_Ao_HandlePagamentoAprovado()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 100m, 1);
            var pagamento = pedido.IniciarPagamento(MetodoPagamento.Pix);

            pedido.HandlePagamentoAprovado(pagamento.Id);

            pedido.StatusPedido.Should().Be(StatusPedido.PagamentoConfirmado);
        }

        [Fact(DisplayName = "Deve manter status pendente ao HandlePagamentoRecusado")]
        public void Deve_Manter_Status_Ao_HandlePagamentoRecusado()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 100m, 1);
            var pagamento = pedido.IniciarPagamento(MetodoPagamento.Pix);

            pedido.HandlePagamentoRejeitado(pagamento.Id);

            //pedido.StatusPedido.Should().Be(StatusPedido.Cancelado);
            pedido.DomainEvents.Should().ContainSingle(e => e is PedidoCanceladoEvent);
        }

        [Fact(DisplayName = "Não deve HandlePagamentoAprovado se status não for Pendente")]
        public void Nao_Deve_HandlePagamentoAprovado_Se_Nao_Pendente()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 100m, 1);
            var pagamento = pedido.IniciarPagamento(MetodoPagamento.Pix);
            SetStatusPedido(pedido, StatusPedido.EmSeparacao);

            Action act = () => pedido.HandlePagamentoAprovado(pagamento.Id);

            act.Should().Throw<DomainException>()
                .WithMessage("O pedido não está no status esperado para confirmação de pagamento.");

        }

        //Transição de estado
        [Fact(DisplayName = "Deve permitir marcar pedido com em Separação após PagamentoConfirmado")]
        public void Deve_Marcar_Como_Em_Separacao()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 100m, 1);
            var pagamento = pedido.IniciarPagamento(MetodoPagamento.CartaoCredito);
            pedido.HandlePagamentoAprovado(pedido.Id);


            //pedido.MarcarComoEmSeparacao();

            //pedido.StatusPedido.Should().Be(StatusPedido.EmSeparacao);
        }

        [Fact(DisplayName = "Não deve marcar com em Separação se não estiver PagamentoConfirmado")]
        public void Nao_Deve_Marcar_Como_Em_Separacao_Se_Nao_Confirmado()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());

            Action act = () => pedido.MarcarComoEmSeparacao();

            act.Should().Throw<DomainException>()
                .WithMessage("O pedido só pode ir pra Separação após o pagamento ser confirmado.");
        }

        [Fact(DisplayName = "Deve marcar como Enviado")]
        public void Deve_Marcar_Como_Enviado()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            SetStatusPedido(pedido, StatusPedido.EmSeparacao);

            pedido.MarcarComoEnviado();

            pedido.StatusPedido.Should().Be(StatusPedido.Enviado);
        }

        [Fact(DisplayName = "Não deve marcar como Enviado se não estiver em Separação")]
        public void Nao_Deve_Marcar_Como_Enviado_Se_Nao_EmSeparacao()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            SetStatusPedido(pedido, StatusPedido.PagamentoConfirmado);

            Action act = () => pedido.MarcarComoEnviado();

            act.Should().Throw<DomainException>()
            .WithMessage("O pedido só pode ser Enviado após estar em separação.");
        }

        [Fact(DisplayName = "Deve marcar pedido como Entregue")]
        public void Deve_Marcar_Como_Entregue()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            SetStatusPedido(pedido, StatusPedido.Enviado);

            pedido.MarcarComoEntregue();

            pedido.StatusPedido.Should().Be(StatusPedido.Entregue);
        }

        [Fact(DisplayName = "Não deve marcar como Entregue se não estiver em Enviado")]
        public void Nao_Deve_Marcar_Como_Entregueo_Se_Nao_Enviado()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            SetStatusPedido(pedido, StatusPedido.EmSeparacao);

            Action act = () => pedido.MarcarComoEntregue();

            act.Should().Throw<DomainException>()
                .WithMessage("O pedido só pode ser marcado como Entregue  após ser Enviado.");
        }

        //Cancelamento
        [Fact(DisplayName = "Deve cancelar pedido Pendente")]
        public void Deve_Cancelar_Pedido_Pendente()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 50m, 1);

            pedido.CancelarPedido();

            pedido.StatusPedido.Should().Be(StatusPedido.Cancelado);
        }

        [Fact(DisplayName = "Deve cancelar pedido PagamentoConfirmado")]
        public void Deve_Cancelar_Pedido_Pagamento_Confirmado()
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            pedido.AdicionarItem(ProdutoIdValido, "Produto", 50m, 1);

            var pagamento = pedido.IniciarPagamento(MetodoPagamento.Pix);

            pedido.CancelarPedido();

            pedido.StatusPedido.Should().Be(StatusPedido.Cancelado);
        }

        [Theory(DisplayName = "Não deve permitior cancelar pedido após em EmSeparacao")]
        [InlineData(StatusPedido.EmSeparacao)]
        [InlineData(StatusPedido.Enviado)]
        [InlineData(StatusPedido.Entregue)]
        public void Nao_Deve_Cancelar_Apos_EmSeparacao(StatusPedido status)
        {
            var pedido = Pedido.Criar(ClientIdValido, CriarEnderecoValido());
            SetStatusPedido(pedido, status);

            Action act = () => pedido.CancelarPedido();

            act.Should().Throw<DomainException>()
                .WithMessage("Não é possível cancelar um pedido que já está em separação ou posteriror.");
        }

    }
}
