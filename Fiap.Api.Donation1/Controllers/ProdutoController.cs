using Fiap.Api.Donation1.Models;
using Fiap.Api.Donation1.Repository.Interface;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository produtoRepository;


        public ProdutoController(IProdutoRepository _produtoRepository)
        {
            produtoRepository = _produtoRepository;
        }

        
        [HttpGet]
        [ApiVersion("2.0", Deprecated =true)]
        public async Task<ActionResult<dynamic>> GetProdutos([FromQuery] int pagina = 0, [FromQuery] int tamanho = 5)
        {

            var totalGeral = await produtoRepository.Count();
            var totalPaginas = Convert.ToInt16(Math.Ceiling((double)totalGeral / tamanho));
            var linkAnterior = (pagina > 0) ? $"/api/produto?pagina={pagina - 1}&tamanho={tamanho}" : "";
            var linkProximo = (pagina < totalPaginas - 1) ? $"/api/produto?pagina={pagina + 1}&tamanho={tamanho}" : "";

            if (pagina > totalPaginas)
            {
                return NotFound();
            }

            var produtos = await produtoRepository.FindAll(pagina, tamanho);
            if (produtos == null || produtos.Count == 0)
            {
                return NoContent();
            }

            var retorno = new
            {
                produtos = produtos,
                total = totalGeral,
                totalPaginas = totalPaginas,
                anterior = linkAnterior,
                proximo = linkProximo
            };

            return Ok(retorno);
        }

        [ApiVersion("3.0")]
        [HttpGet]
        public async Task<ActionResult<dynamic>> GetProdutos([FromQuery] DateTime? dataReferencia = null, [FromQuery] int tamanho = 5)
        {

            dataReferencia = (dataReferencia == null) ? DateTime.UtcNow.AddYears(-200) : dataReferencia;

            var produtos = await produtoRepository.GetAllOrderByDataCadastroAsc(dataReferencia, tamanho);

            if (produtos == null || produtos.Count == 0)
            {
                return NoContent();
            }

            var novaDataReferencia = produtos.LastOrDefault().DataCadastro;

            var retorno = new
            {
                produtos = produtos,
                referencia = novaDataReferencia,
                proximo = $"/api/produto?dataReferencia={novaDataReferencia}"
            };

            return Ok(retorno);

        }

        //[HttpGet("{pagina}")]
        //public async Task<ActionResult<IList<ProdutoModel>>> GetProdutos(int pagina, int tamanho)
        //{
        //    var produtos = await produtoRepository.FindAll(pagina, tamanho);
        //    if (produtos == null || produtos.Count == 0)
        //    {
        //        return NoContent();
        //    }

        //    return Ok(produtos);
        //}

        [HttpGet]
        [ApiVersion("1.0", Deprecated =true)]
        public async Task<ActionResult<IList<ProdutoModel>>> GetProdutos()
        {
           var produtos = await produtoRepository.FindAll();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> GetProdutoModel(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                var prod = await produtoRepository.FindById(id);

                if (prod == null)
                {
                    return NotFound(id);
                }
                else
                {
                    return Ok(prod);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutoModel([FromRoute] int id, ProdutoModel produtoModel)
        {
            if (id != produtoModel.ProdutoId)
            {
                return BadRequest();
            }
            else
            {
               // var produtoConsulta = await produtoRepository.FindById(id);

                if (produtoModel == null)
                {
                    return NotFound();
                }
                else
                {
                    await produtoRepository.Update(produtoModel);
                    return NoContent();
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> PostProdutoModel(ProdutoModel produtoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                await produtoRepository.Insert(produtoModel);

                var url = Request.GetEncodedUrl().EndsWith("/") ?
                                                Request.GetEncodedUrl() :
                                                Request.GetEncodedUrl() + "/";

                var location = new Uri(url + produtoModel.ProdutoId);

                return Created(location, produtoModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdutoModel(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var produto = await produtoRepository.FindById(id);

            if (produto == null)
            {
                return NotFound();
            }
            else
            {
                await produtoRepository.Delete(produto);
                return NoContent();
            }
        }


    }
}
