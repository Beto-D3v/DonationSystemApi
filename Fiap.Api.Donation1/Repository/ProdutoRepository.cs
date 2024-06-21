    using Fiap.Api.Donation1.Data;
using Fiap.Api.Donation1.Models;
using Fiap.Api.Donation1.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation1.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext dataContext;

        public ProdutoRepository(DataContext ctx)
        {
            dataContext = ctx;
        }

        public async Task<int> Count()
        {
            return dataContext.Produtos.Count();
        }

        public async Task<IList<ProdutoModel>> FindAll(int pagina = 0, int tamanho = 10)
        {
            var produtos = dataContext.Produtos
                .Skip(tamanho * pagina)
                .Take(tamanho)
                .ToList();

            return produtos == null ? new List<ProdutoModel>() : produtos;

        }

        public async Task<IList<ProdutoModel>> FindAll()
        {
            var produtos = dataContext.Produtos.ToList();

            return produtos == null ? new List<ProdutoModel>() : produtos;
        }


        public async Task<IList<ProdutoModel>> FindByNome(string nome)
        {
            var produtos = dataContext
                                .Produtos // SELECT * FROM Produtos
                                .Include(p => p.TipoProduto) // INNER JOIN
                                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                                    .ToList();

            return produtos == null ? new List<ProdutoModel>() : produtos;
        }

        public async Task<ProdutoModel> FindById(int id)
        {
            var produto =  dataContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            return produto;
        }

        public async Task<IList<ProdutoModel>> GetAllOrderByDataCadastroAsc(DateTime? dataReferencia, int tamanho)
        {
            var produtos = await dataContext.Produtos
                .Where(p => p.DataCadastro > dataReferencia)
                .OrderBy(p => p.DataCadastro)
                .Take(tamanho)
                .ToListAsync();
             
            return produtos == null ? new List<ProdutoModel>() : produtos;
        }

        // Inserir
        public async Task<int> Insert(ProdutoModel produtoModel)
        {
            dataContext.Produtos.Add(produtoModel);
            dataContext.SaveChanges();

            return produtoModel.ProdutoId;
        }

        public async Task Update(ProdutoModel produtoModel)
        {
            dataContext.Produtos.Update(produtoModel);
            await dataContext.SaveChangesAsync();
        }

        public async Task Delete(ProdutoModel produtoModel)
        {
            dataContext.Produtos.Remove(produtoModel);
            await dataContext.SaveChangesAsync();
        }

    }
}
