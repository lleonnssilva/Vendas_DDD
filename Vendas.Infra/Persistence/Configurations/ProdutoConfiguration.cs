using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain.Catalogo;

namespace Vendas.Infra.Persistence.EntityConfiguration
{

    //public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    //{
    //    public void Configure(EntityTypeBuilder<Produto> builder)
    //    {
    //        builder.ToTable("Produtos");

    //        builder.HasKey(p => p.Id);

    //        builder.OwnsOne(p => p.Nome, nome =>
    //        {
    //            nome.Property(n => n.Valor)
    //                .HasColumnName("Nome")
    //                .HasMaxLength(200)
    //                .IsRequired();
    //        });

    //        builder.OwnsOne(p => p.Codigo, codigo =>
    //        {
    //            codigo.Property(c => c.Valor)
    //                .HasColumnName("Codigo")
    //                .IsRequired();
    //        });

    //        builder.OwnsOne(p => p.Preco, preco =>
    //        {
    //            preco.Property(p => p.Valor)
    //                .HasColumnName("Preco")
    //                .HasColumnType("decimal(18,2)")
    //                .IsRequired();
    //        });


    //        builder.Property(p => p.Descricao)
    //            .HasMaxLength(500);

    //        builder.Property(p => p.CategoriaId)
    //            .IsRequired();

    //        builder.Property(p => p.Estoque)
    //            .IsRequired();

    //        builder.Property(p => p.Status)
    //            .HasConversion<int>()
    //            .IsRequired();

    //        builder.Property(p => p.DataCriacao);
    //        builder.Property(p => p.DataAtualizacao);

    //        builder.OwnsMany(p => p.Imagens, imagens =>
    //        {
    //            imagens.ToTable("ProdutoImagens");

    //            imagens.WithOwner()
    //                   .HasForeignKey("ProdutoId");

    //            imagens.Property<int>("Id"); 
    //            imagens.HasKey("Id");

    //            imagens.Property(i => i.Url)
    //                   .HasMaxLength(500)
    //                   .IsRequired();

    //            imagens.Property(i => i.Ordem)
    //                   .IsRequired();
    //        });

    //        builder.Metadata
    //            .FindNavigation(nameof(Produto.Imagens))!
    //            .SetPropertyAccessMode(PropertyAccessMode.Field);
    //    }
    //}
}
