using Fiap.Api.Donation1.Models;

namespace Fiap.Api.Donation1.Repository.Interface
{
    public interface IProdutoRepository
    {
        public Task<int> Count();

        public Task<IList<ProdutoModel>> FindAll(int pagina, int tamanho);

        public Task<IList<ProdutoModel>> FindAll();

        public Task<IList<ProdutoModel>> FindByNome(string nome);

        public Task<IList<ProdutoModel>> GetAllOrderByDataCadastroAsc(DateTime? dataReferencia, int tamanho);

        public Task<ProdutoModel> FindById(int id);

        public Task<int> Insert(ProdutoModel produtoModel);

        public Task Update(ProdutoModel produtoModel);

        public Task Delete(ProdutoModel produtoModel);

    }
}
