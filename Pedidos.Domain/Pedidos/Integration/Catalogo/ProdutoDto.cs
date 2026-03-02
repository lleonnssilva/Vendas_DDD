namespace Vendas.Domain.Pedidos.Integration.Catalogo
{
    public sealed class ProdutoDto
    {
        public Guid Id { get; }
        public string Nome { get; }
        public decimal Preco { get; }
        public int Quantidade { get; }

        public ProdutoDto(Guid id, string nome, decimal preco, bool ativo, int quantidade)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }
    }
}
