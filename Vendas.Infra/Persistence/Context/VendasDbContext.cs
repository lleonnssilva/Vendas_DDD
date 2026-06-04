using Microsoft.EntityFrameworkCore;
using Vendas.Domain.Pedidos;


namespace Vendas.Infra.Persistence.Context
{
    public class VendasDbContext : DbContext
    {
        //public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        //public DbSet<Categoria> Categorias { get; set; }
        //public DbSet<Produto> Produtos { get; set; }
        //public DbSet<Endereco> Enderecos { get; set; }
        //public DbSet<Estoque> Estoques { get; set; }
        public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendasDbContext).Assembly);

            modelBuilder.Entity<ItemPedido>(item =>
            {
                item.ToTable("ItensPedido");
                item.HasKey(i => i.Id);
                item.Property<Guid>("PedidoId").IsRequired();
                item.Property(i=> i.DataAtualizacao).IsRequired(false);
                item.Ignore(i => i.DomainEvents);
                item.Property(i=> i.NomeProduto).IsRequired().HasMaxLength(200);
                item.Property(i => i.PrecoUnitario).HasPrecision(18,2);
                item.Property(i => i.ValorTotal).HasPrecision(18, 2);
                item.Property(i => i.DescontoAplicado).HasPrecision(18, 2);
            });

        }

    }
}
