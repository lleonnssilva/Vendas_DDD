using Microsoft.AspNetCore.Mvc;
using Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.CriarProduto;
using Vendas.Application.Mediator.Interfaces;

namespace Vendas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        
        private readonly IMediador _mediador;

        public ProdutoController(IMediador mediador)
        {
            _mediador = mediador;
        }

        [HttpPost]
        public async Task<CriarProdutoResultDto> AdicionarAsync(CriarProdutoCommand produto)
        {

            return await _mediador.Send(produto);
        }
    }
}
