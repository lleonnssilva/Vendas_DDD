using Microsoft.AspNetCore.Mvc;
using Vendas.Api.Endpoints.Pedidos;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido;
using Vendas.Application.Commands.PedidosCommands.CancelarPedido;
using Vendas.Application.Commands.PedidosCommands.CriarPedido;
using Vendas.Application.Commands.PedidosCommands.IniciarPagamento;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoEmSeparacao;
using Vendas.Application.Queryes.Pedidos.ListarPedidosPagamentosPorStatus;
using Vendas.Application.Queryes.Pedidos.ListarPedidosResumo;
using Vendas.Application.Queryes.Pedidos.ListarPedidosResumoPorCliente;
using Vendas.Application.Queryes.Pedidos.ObterPedidoCompletoPorId;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos.Enums;

public static class PedidosEndPoints
{
    public static WebApplication MapPedidosEndPoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/pedidos")
            .WithTags("Pedidos");

        group.MapGet("/fakes-ids", () => Results.Ok(new
        {
            clientes = new[]
            {
                    new
                    {
                        clienteId = Guid.Parse("22222222-0000-0000-0000-000000000001"),
                        enderecos = new[]
                    {
                            new
                            {
                                enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000001"),
                                descricao = "Av Paulista 1578, Bela Vista,São Paulo"
                            },
                            new
                            {
                                enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000002"),
                                descricao = "Rua das Flores 300, Vila Olimpia,São Paulo"
                            },
                        }
                    },
                    new
                    {
                        clienteId = Guid.Parse("22222222-0000-0000-0000-000000000002"),
                        enderecos = new[]
                    {
                            new
                            {
                                enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000003"),
                                descricao = "Av do contorno 8000, Santo Agostinho,Belo Horizonte"
                            },
                        }
                    }

                },
            produtos = new[]
        {
                    new
                    {
                        produtoId = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        descricao = "Notebook Gamer Rtx 4060 - R$ 8.500,00"
                    },
                    new
                    {
                        produtoId = Guid.Parse("11111111-0000-0000-0000-000000000002"),
                        descricao = "Mouse Sem Fio Logiteck MX M<aster - R$ 450,00"
                    },
                    new
                    {
                        produtoId = Guid.Parse("11111111-0000-0000-0000-000000000003"),
                        descricao = "Teclado Mecânico Keychoron K8 - R$ 680,00"
                    },
                    new
                    {
                        produtoId = Guid.Parse("11111111-0000-0000-0000-000000000004"),
                        descricao = "Monitor Ultrawide 34 polegadas - R$ 3.200,00"
                    },

                }
        }))
            .WithSummary("Exibe os Ids dos dados disponíveis nos Fakes para usar nos testes");

        //group.MapGet("/", async (IPedidoRepository repo, CancellationToken ct) =>
        //{
        //    var pedidos = await repo.ListarTodosAsync(ct);
        //    var resultado = pedidos.Select(p => new
        //    {
        //        p.Id,
        //        p.NumeroPedido,
        //        p.ClienteId,
        //        p.ValorTotal,
        //        Status = p.StatusPedido.ToString(),
        //        p.DataCriacao,
        //        TotalItens = p.Itens.Count
        //    });

        //    return Results.Ok(resultado);
        //})
        //    .WithSummary("Lista todos pedidos em memória");

        group.MapGet("/", async (
             [FromServices]ListarPedidosResumoQueryHandler handler,
            CancellationToken ct) =>
        {
            var resultado = await handler.HandleAsync(new ListarPedidosResumoQuery(), ct);

            return Results.Ok(resultado);
        })
          .WithSummary("Lista resumida de todos os pedidos");

        group.MapGet("/{id:guid}", async (
            Guid id,
            [FromServices] ObterPedidoCompletoPorIdQueryHandler handler,
            CancellationToken ctl) =>
        {
            var resultado = await handler.HandleAsync(new ObterPedidoCompletoPorIdQuery(id), ctl);
            return resultado is null ? Results.NotFound() : Results.Ok(resultado);

        })
            .WithSummary("Retorna detalhes completos de um pedido");

        group.MapGet("/pagamentos", async ([FromQuery] StatusPagamento? status,
            [FromServices] ListarPedidosPagamentosPorStatusQueryHandler handler,
           CancellationToken ct) =>
        {
            if (status is null)
            {
                return Results.BadRequest(new
                {
                    erro = "Status inválido.Valores aceitos:Pendente(0),Aprovado(1),Recusado(2)"
                });
            }

            var resultado = await handler.HandleAsync(new ListarPedidosPagamentosPorStatusQuery(status.Value), ct);

            return Results.Ok(resultado);

        })
         .WithSummary("Lista pagamentos filtrados por status")
         .WithDescription("Valores válidos para status:\n" +
         " Pendente ou 1\n" +
         " Aprovado ou 2\n" +
         " Cancelado ou 3\n");

        group.MapGet("/pedidos/clientes/{clienteId:guid}", async (
            Guid clienteId,
            [FromServices] ListarPedidosResumoPorClienteQueryHandler handler,
            CancellationToken ctl) =>
        {
            var resultado = await handler.HandleAsync(new ListarPedidosResumoPorClienteQuery(clienteId), ctl);
            return Results.Ok(resultado);

        })
          .WithSummary("Lista de pedidos resumidos de um cliente específico");

        group.MapPost("/", async (
            CriarPedidoRequest req,
            CriarPedidoCommandHandler handler,
            CancellationToken ct) =>
            {
                try
                {
                    var command = new CriarPedidoCommand(req.ClienteId, req.EnderecoId);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Created($"/pedidos/{result.PedidoId}", result);
                }
                catch (InvalidOperationException ex)
                {

                    return Results.NotFound(new { erro = ex.Message });
                }
                catch (DomainException ex)
                {

                    return Results.UnprocessableEntity(new { erro = ex.Message });
                }
            })
                .WithSummary("Criar pedido");

        group.MapPost("/{id:guid}/itens", async (
            Guid id,
            AdicionarItemRequest req,
            AdicionarItemAoPedidoCommandHandler handler,
            CancellationToken ct) =>
        {
            try
            {
                var command = new AdicionarItemAoPedidoCommand(id, req.ProdutoId, req.Quantidade);

                var result = await handler.HandleAsync(command, ct);
                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {

                return Results.NotFound(new { erro = ex.Message });
            }
            catch (DomainException ex)
            {

                return Results.UnprocessableEntity(new { erro = ex.Message });
            }
        })
            .WithSummary("Adicionar item ao pedido");

        group.MapPost("/{id:guid}/pagamento", async (
            Guid id,
            IniciarPagamentoRequest req,
            IniciarPagamentoCommandHandler handler,
            CancellationToken ct) =>
        {
            try
            {
                var metodo = (MetodoPagamento)req.MetodoPagamento;
                var command = new IniciarPagamentoCommand(id, metodo);

                var result = await handler.HandleAsync(command, ct);
                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {

                return Results.NotFound(new { erro = ex.Message });
            }
            catch (DomainException ex)
            {

                return Results.UnprocessableEntity(new { erro = ex.Message });
            }
        })
            .WithSummary("Iniciar o pagamento do pedido");

        group.MapPost("/{id:guid}/pagamento/confirmacao", async (
            Guid id,
            ConfirmarPagamentoRequest req,
            IPedidoRepository repo,
            CancellationToken ct) =>
        {
            try
            {
                var pedido = await repo.ObterPorIdAsync(id);
                if (pedido is null) return Results.NotFound();

                var pagamento = pedido.Pagamentos.FirstOrDefault(p => p.Id == req.PagamentoId);

                if (pagamento is null) return Results.NotFound(new { erro = "Pagamento não encontrado" });

                pagamento.GerarCodigoTransacaoLocal();
                pagamento.ConfirmarPagamento();
                pedido.HandlePagamentoAprovado(pagamento.Id);

                await repo.AtualizarAsync(pedido, ct);
                return Results.Ok(new
                {
                    PedidoId = pedido.Id,
                    PagamentoId = pagamento.Id,
                    StatusPedido = pedido.StatusPedido.ToString(),
                    StatusPagamento = pagamento.StatusPagamento.ToString(),
                    CodigoTransacao = pagamento.CodigoTransacao,
                });
            }
            catch (InvalidOperationException ex)
            {

                return Results.NotFound(new { erro = ex.Message });
            }
            catch (DomainException ex)
            {

                return Results.UnprocessableEntity(new { erro = ex.Message });
            }
        })
            .WithSummary("Confirma o pagamento do pedido")
            .WithDescription("Simulação de gateway de pagamento");

        group.MapPost("/{id:guid}/separacao", async (
           Guid id,
           MarcarPedidoEmSeparacaoCommandHandler handler,
           CancellationToken ct) =>
        {
            try
            {

                var command = new MarcarPedidoEmSeparacaoCommand(id);

                var result = await handler.HandleAsync(command, ct);
                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {

                return Results.NotFound(new { erro = ex.Message });
            }
            catch (DomainException ex)
            {

                return Results.UnprocessableEntity(new { erro = ex.Message });
            }
        })
           .WithSummary("Marca do pedido em separação (Pagamento Confirmado > EmSeparacao)");

        group.MapPost("/{id:guid}/enviado", async (
           Guid id,
           MarcarPedidoComoEnviadoCommandHandler handler,
           CancellationToken ct) =>
        {
            try
            {

                var command = new MarcarPedidoComoEnviadoCommand(id);

                var result = await handler.HandleAsync(command, ct);
                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {

                return Results.NotFound(new { erro = ex.Message });
            }
            catch (DomainException ex)
            {

                return Results.UnprocessableEntity(new { erro = ex.Message });
            }
        })
           .WithSummary("Marca do pedido como enviado (EmSeparacao  > Enviado)");

        group.MapPost("/{id:guid}/entregue", async (
           Guid id,
           MarcarPedidoComoEntregueCommandHandler handler,
           CancellationToken ct) =>
        {
            try
            {

                var command = new MarcarPedidoComoEntregueCommand(id);

                var result = await handler.HandleAsync(command, ct);
                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {

                return Results.NotFound(new { erro = ex.Message });
            }
            catch (DomainException ex)
            {

                return Results.UnprocessableEntity(new { erro = ex.Message });
            }
        })
           .WithSummary("Marca do pedido como entregue");

        group.MapPost("/{id:guid}/cancelamento", async (
           Guid id,
           CancelarPedidoRequest? req,
           CancelarPedidoCommandHandler handler,
           CancellationToken ct) =>
        {
            try
            {

                var command = new CancelarPedidoCommand(id, req?.CodigoMotivo ?? "Outro");

                var result = await handler.HandleAsync(command, ct);
                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {

                return Results.NotFound(new { erro = ex.Message });
            }
            catch (DomainException ex)
            {

                return Results.UnprocessableEntity(new { erro = ex.Message });
            }
        })
           .WithSummary("Cancela o pedido");

        return app;
    }
}

