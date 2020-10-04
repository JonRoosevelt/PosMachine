using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace celcoin.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "Id",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Taxas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposVenda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Saldo = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeiosPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    TaxaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeiosPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeiosPagamento_Taxas_TaxaId",
                        column: x => x.TaxaId,
                        principalTable: "Taxas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendedorId = table.Column<int>(nullable: false),
                    MeioPagamentoId = table.Column<int>(nullable: false),
                    TipoVendaId = table.Column<int>(nullable: false),
                    TaxaParcelaId = table.Column<int>(nullable: false),
                    NumParcelas = table.Column<int>(nullable: false),
                    Recebivel = table.Column<decimal>(nullable: false),
                    ValorVenda = table.Column<decimal>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendas_MeiosPagamento_MeioPagamentoId",
                        column: x => x.MeioPagamentoId,
                        principalTable: "MeiosPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendas_Taxas_TaxaParcelaId",
                        column: x => x.TaxaParcelaId,
                        principalTable: "Taxas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendas_TiposVenda_TipoVendaId",
                        column: x => x.TipoVendaId,
                        principalTable: "TiposVenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendas_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Taxas",
                columns: new[] { "Id", "Nome", "Valor" },
                values: new object[,]
                {
                    { 1000, "taxa_debito", 2.3m },
                    { 1001, "taxa_credito", 4.55m },
                    { 1002, "taxa_parcela_credito", 1.6m },
                    { 1003, "taxa_parcela_debito", 0m }
                });

            migrationBuilder.InsertData(
                table: "TiposVenda",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1000, "CUSTO_VENDEDOR" },
                    { 1001, "PARCELADO_CLIENTE" },
                    { 1002, "CUSTO_CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "Vendedores",
                columns: new[] { "Id", "Nome", "Saldo" },
                values: new object[] { 1000, "Marcos Vinícios de Oliveira", 0.0 });

            migrationBuilder.InsertData(
                table: "MeiosPagamento",
                columns: new[] { "Id", "Nome", "TaxaId" },
                values: new object[,]
                {
                    { 1000, "Débito", 1000 },
                    { 1001, "Crédito", 1001 }
                });

            migrationBuilder.InsertData(
                table: "Vendas",
                columns: new[] { "Id", "Data", "MeioPagamentoId", "NumParcelas", "Recebivel", "TaxaParcelaId", "TipoVendaId", "ValorVenda", "VendedorId" },
                values: new object[,]
                {
                    { 1000, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000, 1, 102.3m, 1000, 1000, 100m, 1000 },
                    { 1001, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1001, 3, 101.67m, 1002, 1001, 100m, 1000 },
                    { 1002, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1001, 4, 106.22m, 1002, 1002, 100m, 1000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeiosPagamento_TaxaId",
                table: "MeiosPagamento",
                column: "TaxaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_MeioPagamentoId",
                table: "Vendas",
                column: "MeioPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_TaxaParcelaId",
                table: "Vendas",
                column: "TaxaParcelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_TipoVendaId",
                table: "Vendas",
                column: "TipoVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VendedorId",
                table: "Vendas",
                column: "VendedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "MeiosPagamento");

            migrationBuilder.DropTable(
                name: "TiposVenda");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropTable(
                name: "Taxas");

            migrationBuilder.DropSequence(
                name: "Id");
        }
    }
}
