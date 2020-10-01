using celcoin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace celcoin.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Taxa> Taxas { get; set; }
        public DbSet<MeioPagamento> MeiosPagamento { get; set; }
        public DbSet<TipoVenda> TiposVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendedor>().HasData(
                new Vendedor { Id = 1, Nome = "Marcos Vinícios de Oliveira", Saldo = 0 }
            );
            modelBuilder.Entity<Taxa>().HasData(
                new Taxa { Id = 1, Nome = "taxa_debito", Valor = 2.3m, },
                new Taxa { Id = 2, Nome = "taxa_credito", Valor = 4.55m, },
                new Taxa { Id = 3, Nome = "taxa_parcela_credito", Valor = 1.6m, },
                new Taxa { Id = 4, Nome = "taxa_parcela_debito", Valor = 0, }
            );
            modelBuilder.Entity<MeioPagamento>().HasData(
                new MeioPagamento { Id = 1, Nome = "Débito", TaxaId = 3 },
                new MeioPagamento { Id = 2, Nome = "Crédito", TaxaId = 4 }
            );
            modelBuilder.Entity<TipoVenda>().HasData(
                new TipoVenda { Id = 1, Nome = "CUSTO_VENDEDOR" },
                new TipoVenda { Id = 2, Nome = "PARCELADO_CLIENTE" },
                new TipoVenda { Id = 3, Nome = "CUSTO_CLIENTE" }
            );
            modelBuilder.Entity<Venda>().HasData(
                new Venda { Id = 1, VendedorId = 1, MeioPagamentoId = 1, TipoVendaId = 1, TaxaParcelaId = 1, NumParcelas = 1, Recebivel = 100 },
                new Venda { Id = 2, VendedorId = 1, MeioPagamentoId = 2, TipoVendaId = 2, TaxaParcelaId = 3, NumParcelas = 4, Recebivel = 100 }
            );
        }
    }
}