using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompreApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClienteCartao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeProprietarioCartao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroCartao = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    MesVencimento = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    AnoVencimento = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    CodigoSeguranca = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    BandeiraCartao = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteCartao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteCartao_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteEndereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteEndereco_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataFinalizacaoPedido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteEnderecoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SituacaoPedido = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_ClienteEndereco_ClienteEnderecoId",
                        column: x => x.ClienteEnderecoId,
                        principalTable: "ClienteEndereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteCartaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoPagamento_ClienteCartao_ClienteCartaoId",
                        column: x => x.ClienteCartaoId,
                        principalTable: "ClienteCartao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoPagamento_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "DataAlteracao", "DataCadastro", "Descricao", "Nome", "Preco", "Situacao" },
                values: new object[,]
                {
                    { new Guid("0959f1c0-426f-427c-8a51-2419f3d9c1e1"), null, new DateTime(2024, 8, 12, 19, 37, 51, 218, DateTimeKind.Utc).AddTicks(4716), "O Adobe Photoshop Lightroom é um software designado a edição rápida e o armazenamento de fotos digitais.", "Lightroom", 150m, 1 },
                    { new Guid("78eeb6d8-73b4-4dda-9e1d-d7b062383828"), null, new DateTime(2024, 8, 12, 19, 37, 51, 218, DateTimeKind.Utc).AddTicks(4716), "O Adobe Photoshop é um software de edição de imagens bidimensionais do tipo raster integrante do pacote Adobe.", "Adobe Photoshop", 400m, 1 },
                    { new Guid("a117f909-80d8-445e-bc80-27228b741118"), null, new DateTime(2024, 8, 12, 19, 37, 51, 218, DateTimeKind.Utc).AddTicks(4716), "Final Cut Pro é um software profissional de edição de vídeo não linear.", "Final Cut Pro", 300m, 1 },
                    { new Guid("ec3775e2-65c9-4ee4-b232-9fa0f976ed0f"), null, new DateTime(2024, 8, 12, 19, 37, 51, 218, DateTimeKind.Utc).AddTicks(4716), "AutoCAD é um software do tipo CAD — computer aided design ou desenho auxiliado por computador.", "AutoCAD", 250m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteCartao_ClienteId",
                table: "ClienteCartao",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteEndereco_ClienteId",
                table: "ClienteEndereco",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ClienteEnderecoId",
                table: "Pedido",
                column: "ClienteEnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ClienteId",
                table: "Pedido",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ProdutoId",
                table: "Pedido",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPagamento_ClienteCartaoId",
                table: "PedidoPagamento",
                column: "ClienteCartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPagamento_PedidoId",
                table: "PedidoPagamento",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoPagamento");

            migrationBuilder.DropTable(
                name: "ClienteCartao");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "ClienteEndereco");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
