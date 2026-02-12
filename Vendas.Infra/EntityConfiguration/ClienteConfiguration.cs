using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain.Clientes;

namespace Vendas.Infra.EntityConfiguration
{

    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
       
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Tabela
            builder.ToTable("Clientes");

            // Chave primária
            builder.HasKey(c => c.Id);

            // Propriedades Value Object
            builder.OwnsOne(c => c.Nome, nome =>
            {
                nome.Property(n => n.NomeCompletoFormatado)
                    .HasColumnName("Nome")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.Cpf, cpf =>
            {
                cpf.Property(c => c.Numero)
                   .HasColumnName("Cpf")
                   .HasMaxLength(11)
                   .IsRequired();
            });

            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Endereco)
                     .HasColumnName("Email")
                     .HasMaxLength(150)
                     .IsRequired();
            });

            builder.OwnsOne(c => c.Telefone, telefone =>
            {
                telefone.Property(t => t.Numero)
                        .HasColumnName("Telefone")
                        .HasMaxLength(20)
                        .IsRequired();
            });

            // Enum como string ou int
            builder.Property(c => c.Status)
                   .HasConversion<string>() // ou .HasConversion<int>()
                   .IsRequired();

            builder.Property(c => c.Sexo)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(c => c.EstadoCivil)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(c => c.EnderecoPrincipalId)
                   .IsRequired();
            builder.Ignore(c => c.Endereco);
            // Coleção de endereços (Owned Entity)
            builder.OwnsMany(c => c.Enderecos, endereco =>
            {
                endereco.WithOwner().HasForeignKey("ClienteId");
                endereco.ToTable("Enderecos");

                endereco.HasKey(e => e.Id);

                endereco.Property(e => e.Logradouro)
                        .HasMaxLength(200)
                        .IsRequired();

                endereco.Property(e => e.Cep)
                        .HasMaxLength(20)
                        .IsRequired();

                endereco.Property(e => e.Numero)
                        .HasMaxLength(20)
                        .IsRequired();

                endereco.Property(e => e.Bairro)
                        .HasMaxLength(100)
                        .IsRequired();

                endereco.Property(e => e.Cidade)
                        .HasMaxLength(100)
                        .IsRequired();

                endereco.Property(e => e.Estado)
                        .HasMaxLength(50)
                        .IsRequired();

                endereco.Property(e => e.Pais)
                        .HasMaxLength(50)
                        .IsRequired();

                endereco.Property(e => e.Complemento)
                        .HasMaxLength(250);
            });

            // Colunas de auditoria (caso existam na AggregateRoot)
            builder.Property<DateTime>("DataCriacao")
                   .IsRequired();

            builder.Property<DateTime?>("DataAtualizacao");
        }

    }
}

