using Vendas.Domain.Common.Base;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Common.Validations;

namespace Vendas.Domain.Catalogo.ValueObjects
{

    public sealed class ImagemProduto : ValueObject
    {

        public string Url { get; }
        public int Ordem { get; }
        public Guid ProdutoId { get; }
        private ImagemProduto()
        {
        }
        public ImagemProduto(string url, int ordem,Guid produtoId)
        {
            Guard.AgainstNullOrWhiteSpace(url, nameof(url));
            Guard.Against<DomainException>(ordem < 1, "A ordem da imagem deve ser >=1.");


            Url = url;
            Ordem = ordem;
            ProdutoId = produtoId;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return Ordem;
        }
    }
}
