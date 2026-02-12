using Microsoft.AspNetCore.Mvc;
using Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido;
using Vendas.Application.Commands.PedidosCommands.CriarPedido;
using Vendas.Application.Commands.PedidosCommands.IniciarPagamento;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoPago;
using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {

        private readonly IMediador _mediador;

        public PedidoController(IMediador mediador)
        {
            _mediador = mediador;
        }

        [HttpPost("CriarPedido")]
        public async Task<CriarPedidoResultDto> AdicionarAsync(CriarPedidoCommand pedido)
        {
            return await _mediador.Send(pedido);
        }
       
        [HttpPost("AdicionarItem")]
        public async Task<AdicionarItemAoPedidoResultDto> AdicionarItemAsync(AdicionarItemAoPedidoCommand pedido)
        {
            return await _mediador.Send(pedido);
        }

        [HttpPost("RealizarPagamento")]
        public async Task<IniciarPagamentoResultDto> RealizarPagamentoAsync(IniciarPagamentoCommand pagamento)
        {
            return await _mediador.Send(pagamento);
        }

        [HttpPost("AprovarPagamento")]
        public async Task<MarcarPedidoComoPagoResultDto> AprovarPagamentoAsync(MarcarPedidoComoPagoCommand pagamento)
        {
            return await _mediador.Send(pagamento);
        }

        [HttpPost("EnviarPedido")]
        public async Task<MarcarPedidoComoEnviadoResultDto> EnviarPedido(MarcarPedidoComoEnviadoCommand pedido)
        {
            return await _mediador.Send(pedido);
        }


        [HttpPost("EntregarPedido")]
        public async Task<MarcarPedidoComoEntregueResultDto> EntregarPedido(MarcarPedidoComoEntregueCommand pedido)
        {
            return await _mediador.Send(pedido);
        }
    }
}
