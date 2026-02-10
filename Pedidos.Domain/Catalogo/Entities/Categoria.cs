using Vendas.Domain.Catalogo.Events;
using Vendas.Domain.Common.Base;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Common.Validations;

namespace Vendas.Domain.Catalogo
{
    public sealed class Categoria : AggregateRoot
    {
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public bool Ativa { get; private set; }

        public Categoria(string nome, string? descricao = null)
        {
            Guard.AgainstNullOrWhiteSpace(nome, nameof(nome), "Nome é obrigatório.");
            Guard.Against<DomainException>(nome.Length < 3, "Nome deve ter pelo menos 3 caracteres.");

            Nome = nome;
            Descricao = descricao;
            Ativa = true;

        }

        public void AlterarNome(string novoNome)
        {
            Guard.AgainstNullOrWhiteSpace(novoNome, nameof(novoNome), "Nome é obbrigatório.");
            Guard.Against<DomainException>(novoNome.Length < 3, "Nome deve ter pelo menos 3 caracteres.");

            Nome = novoNome.Trim();
            SetDataAtualizacao();
        }

        public void AlterarDescricao(string? novaCategoria)
        {
            Descricao = novaCategoria;
            SetDataAtualizacao() ;
        }

        public void Ativar()
        {
            Guard.Against<DomainException>(Ativa, "Categoria já ativa.");

            Ativa =true;
            SetDataAtualizacao();
            AddDomainEvent(new CategoriaAtivadaEvent(Id));
        }

        public void Inativar()
        {
            Guard.Against<DomainException>(!Ativa, "Categoria já está inátiva.");

            Ativa = false;
            SetDataAtualizacao();
            AddDomainEvent(new CategoriaInativadaEvent(Id));
        }

    }
}
