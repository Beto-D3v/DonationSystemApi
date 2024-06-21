using Fiap.Api.Donation1.Models;
using Fiap.Api.Donation1.Repository.Interface;

namespace Fiap.Api.Donation1.Services
{
    public class TrocaService
    {
        private ITrocaRepository trocaRepository;
        private IProdutoRepository produtoRepository;

        public TrocaService(ITrocaRepository _trocaRepository, IProdutoRepository _produtoRepository)
        {
            trocaRepository = _trocaRepository;
            produtoRepository = _produtoRepository;
        }

        public async Task<Guid> TrocarProdutos(TrocaModel trocaModel)
        {
            var produto1 = await produtoRepository.FindById(trocaModel.ProdutoId1); //Eu quero

            var produto2 = await produtoRepository.FindById(trocaModel.ProdutoId2); //Meu produto

            if (produto1.Disponivel == false)
            {
                throw new Exception("Este produto não está disponível!");
            }

            if (produto2.Disponivel == false)
            {
                throw new Exception("Este produto não está disponível!");
            }

            if(produto1.UsuarioId == trocaModel.UsuarioId)
            {
                throw new Exception("Esse produto não pode ser escolhido pelo usuário da troca");
            }

            if (produto2.UsuarioId != trocaModel.UsuarioId)
            {
                throw new Exception("Não é possível trocar um produto de outro usuário");
            }

            if ((produto2.Valor / produto1.Valor) < 0.9)
            {
                throw new Exception("O valor do produto que você está tentando trocar é menor do que 90% do valor do outro produto!");
            }

            produto1.Disponivel = false;
            await produtoRepository.Update(produto1);

            produto2.Disponivel = false;
            await produtoRepository.Update(produto2);

            trocaModel.Status = TrocaStatus.Iniciado;
            trocaRepository.Insert(trocaModel);

            return new Guid();

        }

    }

}

