using Microsoft.AspNetCore.Mvc;
using Vendas.Application.Commands.EstoqueCommands.AdicionarItemAoEstoque;
using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : Controller
    {

        private readonly IMediador _mediador;

        public EstoqueController(IMediador mediador)
        {
            _mediador = mediador;
        }


        [HttpPost]
        public async Task<AdicionarItemAoEstoqueResultDto> AdicionarAsync(AdicionarItemAoEstoqueCommand estoque)
        {

            return await _mediador.Send(estoque);
        }
    }
}
