using AutoMapper;
using Fiap.Api.Donation1.Data;
using Fiap.Api.Donation1.Models;
using Fiap.Api.Donation1.Repository;
using Fiap.Api.Donation1.Repository.Interface;
using Fiap.Api.Donation1.Services;
using Fiap.Api.Donation1.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Fiap.Api.Donation1.Controllers
{
    [Authorize]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TrocaController : ControllerBase
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly ITrocaRepository trocaRepository;
        private readonly IMapper mapper;
        private readonly TrocaService trocaService;

        public TrocaController(ITrocaRepository _trocaRepository, IProdutoRepository _produtoRepository, IMapper _mapper, DataContext dataContext)
        {
            produtoRepository = _produtoRepository;
            trocaRepository = _trocaRepository;
            mapper = _mapper;
            trocaService = new TrocaService(trocaRepository, produtoRepository);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrocaResponseVM>> Get(Guid id)
        {
            var trocaModel = trocaRepository.FindById(id);
            var trocaResponseVM = mapper.Map<TrocaResponseVM>(trocaModel);
            trocaResponseVM.Produto1 = mapper.Map<ProdutoResponseVM>(trocaModel.ProdutoModel1);
            trocaResponseVM.Produto2 = mapper.Map<ProdutoResponseVM>(trocaModel.ProdutoModel2);
            return Ok(trocaResponseVM);
        }


   
        public async Task<ActionResult<Guid>> Post(TrocaRequestVM trocaRequestVM)
        {

            try
            {

                var trocaModel = mapper.Map<TrocaModel>(trocaRequestVM);

                trocaModel.UsuarioId = (int)GetUsuarioId();

                var retorno = trocaService.TrocarProdutos(trocaModel);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var erro = new
                {
                    ErrorMessage = ex.Message,
                };

                return BadRequest(erro);
            }



        }

        private int? GetUsuarioId()
        {
            int? userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userIdClaim = identity.FindFirst("UsuarioId");
                if (userIdClaim != null && userIdClaim.Value != null)
                {
                    userId = Int16.Parse(userIdClaim.Value);
                }
                
            }
            return userId;
        }



    }
}
