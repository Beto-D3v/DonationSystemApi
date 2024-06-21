using Fiap.Api.Donation1.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation1.Data
{
    public class DataContext : DbContext
    {

        public DbSet<TipoProdutoModel> TipoProdutos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DbSet<ProdutoModel> Produtos { get; set; }

        public DbSet<TrocaModel> Trocas { get; set; }





        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TipoProdutoModel>().HasData(
                new TipoProdutoModel(1,"Celular","Descrição para celular"),
                new TipoProdutoModel(2,"TVs","Descrição para TV"),
                new TipoProdutoModel(3,"Ar-condicionado","Descrição para ar-condicionado")
            );


            modelBuilder.Entity<UsuarioModel>().HasData(
                new UsuarioModel(1,"sa@fiap.com.br","Super Admin","123456", "admin"),
                new UsuarioModel(2, "fmoreni@fiap.com.br", "Flavio Moreni", "123456", "admin"),
                new UsuarioModel(3, "emoreni@fiap.com.br", "Eduardo Moreni", "123456", "admin")
            );

            modelBuilder.Entity<ProdutoModel>().HasData(
                new ProdutoModel(1, "Produto 1", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(2, "Produto 2", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(3, "Produto 3", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(4, "Produto 4", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(5, "Produto 5", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(6, "Produto 6", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(7, "Produto 7", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(8, "Produto 8", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(9, "Produto 9", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(10, "Produto 10", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(11, "Produto 11", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(12, "Produto 12", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(13, "Produto 13", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(14, "Produto 14", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(15, "Produto 15", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(16, "Produto 16", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(17, "Produto 17", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(18, "Produto 18", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(19, "Produto 19", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(20, "Produto 20", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(21, "Produto 21", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(22, "Produto 22", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(23, "Produto 23", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(24, "Produto 24", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(25, "Produto 25", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(26, "Produto 26", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(27, "Produto 27", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(28, "Produto 28", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(29, "Produto 29", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(30, "Produto 30", true, "Descrição", "Sugestão de troca", 1, 1, 1),
                new ProdutoModel(31, "Produto 31", true, "Descrição", "Sugestão de troca", 1, 1, 1)

            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
