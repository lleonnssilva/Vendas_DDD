using Microsoft.AspNetCore.Mvc;
using Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.CriarCategoria;
using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CategoriaController : ControllerBase
    {
        private readonly IMediador _mediador;

        public CategoriaController(IMediador mediador)
        {
            _mediador = mediador;
        }

        //[HttpGet]
        //public async Task<CriarCategoriaResultDto> ObterPorIdAsync(Guid categoriaId)
        //{

        //    return await _mediador.Send(categoria);
        //}

        [HttpPost]
        public async Task<CriarCategoriaResultDto> AdicionarAsync(CriarCategoriaCommand categoria)
        {

            return await _mediador.Send(categoria);
        }
    }
}
