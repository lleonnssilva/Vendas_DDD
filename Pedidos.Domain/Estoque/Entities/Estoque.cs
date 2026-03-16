using Vendas.Domain.Common.Base;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Domain.Estoque.Entities
{
    public sealed class Estoque : AggregateRoot
    {
        public Guid ProdutoId { get; private set; }
        public int QuantidadeDisponivel { get; private set; }
        public int QuantidadeReservada { get; private set; }

        protected Estoque() { }

        public Estoque(Guid produtoId,int quantidadeDisponivel, int quantidadeReservada)
        {
            ProdutoId = produtoId;
            QuantidadeDisponivel = quantidadeDisponivel;
            QuantidadeReservada = quantidadeReservada;
        }

        public void AdicionarItemEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new DomainException("Quantidade inválida");

            QuantidadeDisponivel += quantidade;
        }

        public void ReservarItemEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new DomainException("Quantidade inválida");

            if (QuantidadeDisponivel < quantidade)
                throw new DomainException("Estoque insuficiente");

            QuantidadeDisponivel -= quantidade;
            QuantidadeReservada += quantidade;
        }

        public void ConfirmarReservaEstoque(int quantidade)
        {
            QuantidadeReservada -= quantidade;
        }

        public void CancelarReservaEstoque(int quantidade)
        {
            QuantidadeReservada -= quantidade;
            QuantidadeDisponivel += quantidade;
        }
    }
}
