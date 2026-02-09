using Microsoft.EntityFrameworkCore;
using Vendas.Domain.Catalogo;
using Vendas.Domain.Clientes;
using Vendas.Domain.Pedidos;

namespace Vendas.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new ClienteConfiguration());

        //}

    }
}
