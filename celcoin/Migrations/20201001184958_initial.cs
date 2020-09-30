using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace celcoin.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    nome = table.Column<string>(maxLength: 200, nullable: false)
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
        }
    }
}
