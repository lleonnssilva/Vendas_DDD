using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain.Estoque.Entities;

namespace Vendas.Infra.EntityConfiguration
{
    public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ProdutoId);
            builder.Property(p => p.QuantidadeDisponivel);
            builder.Property(p => p.QuantidadeReservada);
            builder.Property(p => p.DataCriacao);
            builder.Property(p => p.DataAtualizacao);
        }
    }
}
