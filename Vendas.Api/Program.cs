using Microsoft.EntityFrameworkCore;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.CriarCategoria;
using Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.CriarProduto;
using Vendas.Application.Commands.ClientesCommands.CriarCliente;
using Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido;
using Vendas.Application.Commands.PedidosCommands.CriarPedido;
using Vendas.Application.Commands.PedidosCommands.IniciarPagamento;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoPago;
using Vendas.Application.Mediator.Implementation;
using Vendas.Application.Mediator.Interfaces;
using Vendas.Infra.Context;
using Vendas.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMediador, Mediador>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IRequestHandler<CriarCategoriaCommand, CriarCategoriaResultDto>, CriarCategoriaCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CriarProdutoCommand, CriarProdutoResultDto>, CriarProdutoCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CriarClienteCommand, CriarClienteResultDto>, CriarClienteCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CriarPedidoCommand, CriarPedidoResultDto>, CriarPedidoCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AdicionarItemAoPedidoCommand, AdicionarItemAoPedidoResultDto>, AdicionarItemAoPedidoCommandHandler>();
builder.Services.AddScoped<IRequestHandler<IniciarPagamentoCommand, IniciarPagamentoResultDto>, IniciarPagamentoCommandHandler>();
builder.Services.AddScoped<IRequestHandler<MarcarPedidoComoPagoCommand, MarcarPedidoComoPagoResultDto>, MarcarPedidoComoPagoCommandHandler>();
builder.Services.AddScoped<IRequestHandler<MarcarPedidoComoEntregueCommand, MarcarPedidoComoEntregueResultDto>, MarcarPedidoComoEntregueCommandHandler>();
builder.Services.AddScoped<IRequestHandler<MarcarPedidoComoEnviadoCommand, MarcarPedidoComoEnviadoResultDto>, MarcarPedidoComoEnviadoCommandHandler>();

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
