using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain.Pedidos;

namespace Vendas.Infra.EntityConfiguration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            // =====================================================
            // PRIMARY KEY
            // =====================================================
            builder.HasKey(p => p.Id);

            // =====================================================
            // PROPRIEDADES SIMPLES
            // =====================================================
            builder.Property(p => p.ClienteId)
                   .IsRequired();

            builder.Property(p => p.NumeroPedido)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasIndex(p => p.NumeroPedido)
                   .IsUnique();

            builder.Property(p => p.ValorTotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.StatusPedido)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            // =====================================================
            // VALUE OBJECT - ENDERECO ENTREGA
            // =====================================================
            //builder.OwnsOne(p => p.EnderecoEntrega, endereco =>
            //{
            //    endereco.Property(e => e.Cep)
            //            .HasColumnName("Cep")
            //            .HasMaxLength(20)
            //            .IsRequired();

            //    endereco.Property(e => e.Logradouro)
            //            .HasColumnName("Logradouro")
            //            .HasMaxLength(200)
            //            .IsRequired();

            //    endereco.Property(e => e.Numero)
            //            .HasColumnName("Numero")
            //            .HasMaxLength(20)
            //            .IsRequired();

            //    endereco.Property(e => e.Bairro)
            //            .HasColumnName("Bairro")
            //            .HasMaxLength(100)
            //            .IsRequired();

            //    endereco.Property(e => e.Cidade)
            //            .HasColumnName("Cidade")
            //            .HasMaxLength(100)
            //            .IsRequired();

            //    endereco.Property(e => e.Estado)
            //            .HasColumnName("Estado")
            //            .HasMaxLength(50)
            //            .IsRequired();

            //    endereco.Property(e => e.Pais)
            //            .HasColumnName("Pais")
            //            .HasMaxLength(50)
            //            .IsRequired();

            //    endereco.Property(e => e.Complemento)
            //            .HasColumnName("Complemento")
            //            .HasMaxLength(250);

            //    endereco.WithOwner();
            //});

            // =====================================================
            // IGNORAR PROPRIEDADES READONLY
            // =====================================================
            builder.Ignore(p => p.Itens);
            builder.Ignore(p => p.Pagamentos);

            // =====================================================
            // ITENS (Backing Field)
            // =====================================================
            builder.OwnsMany<ItemPedido>("_itens", item =>
            {
                item.ToTable("PedidoItens");

                item.WithOwner()
                    .HasForeignKey("PedidoId");

                item.HasKey("Id");

                item.Property<Guid>("Id")
                    .ValueGeneratedNever();

                item.Property(i => i.ProdutoId)
                    .IsRequired();

                item.Property(i => i.NomeProduto)
                    .HasMaxLength(200)
                    .IsRequired();

                item.Property(i => i.PrecoUnitario)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                item.Property(i => i.Quantidade)
                    .IsRequired();

                item.Property(i => i.ValorTotal)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            // =====================================================
            // PAGAMENTOS (Backing Field)
            // =====================================================
            builder.OwnsMany<Pagamento>("_pagamentos", pagamento =>
            {
                pagamento.ToTable("PedidoPagamentos");

                pagamento.WithOwner()
                         .HasForeignKey("PedidoId");

                pagamento.HasKey("Id");

                pagamento.Property<Guid>("Id")
                         .ValueGeneratedNever();

                pagamento.Property(p => p.MetodoPagamento)
                         .HasConversion<string>()
                         .HasMaxLength(30)
                         .IsRequired();

                pagamento.Property(p => p.StatusPagamento)
                         .HasConversion<string>()
                         .HasMaxLength(30)
                         .IsRequired();

                pagamento.Property(p => p.Valor)
                         .HasColumnType("decimal(18,2)")
                         .IsRequired();

                pagamento.Property<DateTime>("DataCriacao")
                         .IsRequired();
            });

            // =====================================================
            // AUDITORIA (AggregateRoot)
            // =====================================================
            builder.Property<DateTime>("DataCriacao")
                   .IsRequired();

            builder.Property<DateTime?>("DataAtualizacao");
        }
    }
}
