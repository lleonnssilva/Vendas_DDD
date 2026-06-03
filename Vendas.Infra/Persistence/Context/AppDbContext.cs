using Microsoft.EntityFrameworkCore;
using Vendas.Domain.Catalogo;
using Vendas.Domain.Clientes;
using Vendas.Domain.Estoque.Entities;
using Vendas.Domain.Pedidos;
using Vendas.Infra.Persistence.EntityConfiguration;


namespace Vendas.Infra.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{
        //}
        //public DbSet<Cliente> Clientes { get; set; }
        //public DbSet<Pedido> Pedidos { get; set; }
        //public DbSet<Categoria> Categorias { get; set; }
        //public DbSet<Produto> Produtos { get; set; }
        //public DbSet<Endereco> Enderecos { get; set; }
        //public DbSet<Estoque> Estoques { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        //    modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        //    modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        //    modelBuilder.ApplyConfiguration(new PedidoConfiguration());
        //    modelBuilder.ApplyConfiguration(new EstoqueConfiguration());
        //}

    }
}
