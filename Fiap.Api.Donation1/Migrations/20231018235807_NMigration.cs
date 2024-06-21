using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fiap.Api.Donation1.Migrations
{
    /// <inheritdoc />
    public partial class NMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoProduto",
                columns: table => new
                {
                    TipoProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProduto", x => x.TipoProdutoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmailUsuario = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Regra = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SugestaoTroca = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TipoProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produto_TipoProduto_TipoProdutoId",
                        column: x => x.TipoProdutoId,
                        principalTable: "TipoProduto",
                        principalColumn: "TipoProdutoId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Produto_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Troca",
                columns: table => new
                {
                    TrocaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProdutoId1 = table.Column<int>(type: "int", nullable: false),
                    ProdutoId2 = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troca", x => x.TrocaId);
                    table.ForeignKey(
                        name: "FK_Troca_Produto_ProdutoId1",
                        column: x => x.ProdutoId1,
                        principalTable: "Produto",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Troca_Produto_ProdutoId2",
                        column: x => x.ProdutoId2,
                        principalTable: "Produto",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "TipoProduto",
                columns: new[] { "TipoProdutoId", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Descrição para celular", "Celular" },
                    { 2, "Descrição para TV", "TVs" },
                    { 3, "Descrição para ar-condicionado", "Ar-condicionado" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioId", "EmailUsuario", "NomeUsuario", "Regra", "Senha" },
                values: new object[,]
                {
                    { 1, "sa@fiap.com.br", "Super Admin", "admin", "123456" },
                    { 2, "fmoreni@fiap.com.br", "Flavio Moreni", "admin", "123456" },
                    { 3, "emoreni@fiap.com.br", "Eduardo Moreni", "admin", "123456" }
                });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "ProdutoId", "DataCadastro", "DataExpiracao", "Descricao", "Disponivel", "Nome", "SugestaoTroca", "TipoProdutoId", "UsuarioId", "Valor" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9866), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9867), "Descrição", true, "Produto 1", "Sugestão de troca", 1, 1, 1.0 },
                    { 2, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9878), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9878), "Descrição", true, "Produto 2", "Sugestão de troca", 1, 1, 1.0 },
                    { 3, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9881), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9882), "Descrição", true, "Produto 3", "Sugestão de troca", 1, 1, 1.0 },
                    { 4, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9885), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9886), "Descrição", true, "Produto 4", "Sugestão de troca", 1, 1, 1.0 },
                    { 5, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9888), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9889), "Descrição", true, "Produto 5", "Sugestão de troca", 1, 1, 1.0 },
                    { 6, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9892), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9893), "Descrição", true, "Produto 6", "Sugestão de troca", 1, 1, 1.0 },
                    { 7, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9974), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9974), "Descrição", true, "Produto 7", "Sugestão de troca", 1, 1, 1.0 },
                    { 8, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9978), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9978), "Descrição", true, "Produto 8", "Sugestão de troca", 1, 1, 1.0 },
                    { 9, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9981), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9982), "Descrição", true, "Produto 9", "Sugestão de troca", 1, 1, 1.0 },
                    { 10, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9984), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9985), "Descrição", true, "Produto 10", "Sugestão de troca", 1, 1, 1.0 },
                    { 11, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9987), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9988), "Descrição", true, "Produto 11", "Sugestão de troca", 1, 1, 1.0 },
                    { 12, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9990), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9991), "Descrição", true, "Produto 12", "Sugestão de troca", 1, 1, 1.0 },
                    { 13, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9993), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9994), "Descrição", true, "Produto 13", "Sugestão de troca", 1, 1, 1.0 },
                    { 14, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9996), new DateTime(2025, 4, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9997), "Descrição", true, "Produto 14", "Sugestão de troca", 1, 1, 1.0 },
                    { 15, new DateTime(2023, 10, 18, 20, 58, 7, 513, DateTimeKind.Local).AddTicks(9999), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local), "Descrição", true, "Produto 15", "Sugestão de troca", 1, 1, 1.0 },
                    { 16, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(3), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(3), "Descrição", true, "Produto 16", "Sugestão de troca", 1, 1, 1.0 },
                    { 17, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(6), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(7), "Descrição", true, "Produto 17", "Sugestão de troca", 1, 1, 1.0 },
                    { 18, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(9), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(9), "Descrição", true, "Produto 18", "Sugestão de troca", 1, 1, 1.0 },
                    { 19, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(12), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(13), "Descrição", true, "Produto 19", "Sugestão de troca", 1, 1, 1.0 },
                    { 20, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(15), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(16), "Descrição", true, "Produto 20", "Sugestão de troca", 1, 1, 1.0 },
                    { 21, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(18), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(19), "Descrição", true, "Produto 21", "Sugestão de troca", 1, 1, 1.0 },
                    { 22, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(21), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(22), "Descrição", true, "Produto 22", "Sugestão de troca", 1, 1, 1.0 },
                    { 23, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(24), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(25), "Descrição", true, "Produto 23", "Sugestão de troca", 1, 1, 1.0 },
                    { 24, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(27), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(28), "Descrição", true, "Produto 24", "Sugestão de troca", 1, 1, 1.0 },
                    { 25, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(31), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(32), "Descrição", true, "Produto 25", "Sugestão de troca", 1, 1, 1.0 },
                    { 26, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(34), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(35), "Descrição", true, "Produto 26", "Sugestão de troca", 1, 1, 1.0 },
                    { 27, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(37), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(38), "Descrição", true, "Produto 27", "Sugestão de troca", 1, 1, 1.0 },
                    { 28, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(40), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(41), "Descrição", true, "Produto 28", "Sugestão de troca", 1, 1, 1.0 },
                    { 29, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(43), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(44), "Descrição", true, "Produto 29", "Sugestão de troca", 1, 1, 1.0 },
                    { 30, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(46), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(47), "Descrição", true, "Produto 30", "Sugestão de troca", 1, 1, 1.0 },
                    { 31, new DateTime(2023, 10, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(49), new DateTime(2025, 4, 18, 20, 58, 7, 514, DateTimeKind.Local).AddTicks(50), "Descrição", true, "Produto 31", "Sugestão de troca", 1, 1, 1.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_DataCadastro",
                table: "Produto",
                column: "DataCadastro");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_TipoProdutoId",
                table: "Produto",
                column: "TipoProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_UsuarioId",
                table: "Produto",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_ProdutoId1",
                table: "Troca",
                column: "ProdutoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_ProdutoId2",
                table: "Troca",
                column: "ProdutoId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Troca");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "TipoProduto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
