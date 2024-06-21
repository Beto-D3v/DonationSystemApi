using Fiap.Api.Donation1.Models;

namespace Fiap.Api.Donation1.ViewModel
{
    public class TrocaResponseVM
    {
        public Guid TrocaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public TrocaStatus TrocaStatus { get; set; }
        public int UsuarioId { get; set; }
        public ProdutoResponseVM Produto1 {  get; set; }
        public ProdutoResponseVM Produto2 { get; set; }

    }
}
