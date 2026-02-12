using Microsoft.AspNetCore.Mvc;
using Vendas.Application.Commands.ClientesCommands.CriarCliente;
using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {

        private readonly IMediador _mediador;

        public ClienteController(IMediador mediador)
        {
            _mediador = mediador;
        }


        [HttpPost]
        public async Task<CriarClienteResultDto> AdicionarAsync(CriarClienteCommand cliente)
        {

            return await _mediador.Send(cliente);
        }
    }
}