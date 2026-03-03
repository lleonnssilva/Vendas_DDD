using Microsoft.EntityFrameworkCore;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Mediator.Extensions;
using Vendas.Domain.Pedidos.Integration.Catalogo;
using Vendas.Domain.Pedidos.Integration.Cliente;
using Vendas.Infra.Context;
using Vendas.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediator();

builder.Services.AddScoped<CatalogoAcl>();
builder.Services.AddScoped<ClienteAcl>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ICatalogoGateway, CatalogoGatewayRepository>();
builder.Services.AddScoped<IClienteGateway, ClienteGatewayRepository>();
builder.Services.AddScoped<IEstoqueRepository, EstoqueRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
