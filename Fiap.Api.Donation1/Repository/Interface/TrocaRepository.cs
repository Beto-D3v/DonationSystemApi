using Fiap.Api.Donation1.Data;
using Fiap.Api.Donation1.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation1.Repository.Interface
{
    public class TrocaRepository : ITrocaRepository
    {
        private readonly DataContext dataContext;
        
        public TrocaRepository(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public TrocaModel FindById(Guid id)
        {
            var troca = dataContext.Trocas.Include(t => t.ProdutoModel1).Include(t => t.ProdutoModel2).FirstOrDefault(t => t.TrocaId == id);
            return troca;
        }

        public Guid Insert(TrocaModel trocaModel)
        {
            dataContext.Trocas.Add(trocaModel);
            dataContext.SaveChanges();
            return trocaModel.TrocaId;
        }
    }
}
