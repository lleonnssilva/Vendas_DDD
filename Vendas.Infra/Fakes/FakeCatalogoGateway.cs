using Vendas.Domain.Pedidos.Integration.Catalogo;

namespace Vendas.Infra.Fakes
{
    sealed class FakeCatalogoGateway : ICatalogoGateway
    {
        private static readonly Dictionary<Guid, ProdutoDto> _produtos = new()
        {
            [new Guid("11111111-0000-0000-0000-000000000001")] =
                new(new Guid("11111111-0000-0000-0000-000000000001"),
                    "Notebook Gammer Rtx 4060", 8500.00m),

            [new Guid("11111111-0000-0000-0000-000000000002")] =
                new(new Guid("11111111-0000-0000-0000-000000000002"),
                    "Mouse sem fio Logiteck", 450.00m),

            [new Guid("11111111-0000-0000-0000-000000000003")] =
                new(new Guid("11111111-0000-0000-0000-000000000003"),
                    "teclado Mecânico Keychron K8", 680.00m),

            [new Guid("11111111-0000-0000-0000-000000000004")] =
                new(new Guid("11111111-0000-0000-0000-000000000004"),
                    "Monitor UltraWide 34 polegadas", 3200.00m),

        };
        public Task<ProdutoDto?> ObterProdutoPorIdAsync(
            Guid produtoId, 
            CancellationToken cancellationToken = default)
        {
            _produtos.TryGetValue(produtoId, out var produto);
            return Task.FromResult(produto);
        }

        public Task<bool> PossuiEstoqueDisponivelAsync(Guid produtoId, int quantidade, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
