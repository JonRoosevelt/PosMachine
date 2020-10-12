using PosMachine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace PosMachine.Data
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
            modelBuilder.HasSequence<int>("Id")
                .StartsAt(1000);
            modelBuilder.Entity<Vendedor>().HasData(
                new Vendedor { Id = 1000, Nome = "Marcos Vinícios de Oliveira", Saldo = 0 }
            );
            modelBuilder.Entity<Taxa>().HasData(
                new Taxa { 
                    Id = 1000, 
                    Nome = "taxa_debito", 
                    Valor = 2.3m, 
                },
                new Taxa { 
                    Id = 1001, 
                    Nome = "taxa_credito", 
                    Valor = 4.55m, 
                },
                new Taxa { 
                    Id = 1002, 
                    Nome = "taxa_parcela_credito", 
                    Valor = 1.6m, 
                },
                new Taxa { 
                    Id = 1003, 
                    Nome = "taxa_parcela_debito", 
                    Valor = 0,
                 }
            );
            modelBuilder.Entity<MeioPagamento>().HasData(
                new MeioPagamento { 
                    Id = 1000, 
                    Nome = "Débito", 
                    TaxaId = 1000
                },
                new MeioPagamento { 
                    Id = 1001, 
                    Nome = "Crédito", 
                    TaxaId = 1001
                }
            );
            modelBuilder.Entity<TipoVenda>().HasData(
                new TipoVenda { 
                    Id = 1000, 
                    Nome = "CUSTO_VENDEDOR"
                },
                new TipoVenda { 
                    Id = 1001, 
                    Nome = "PARCELADO_CLIENTE"
                },
                new TipoVenda { 
                    Id = 1002, 
                    Nome = "CUSTO_CLIENTE"
                }
            );
            modelBuilder.Entity<Venda>().HasData(
                new Venda { 
                    Id = 1000, 
                    VendedorId = 1000, 
                    MeioPagamentoId = 1000, 
                    TipoVendaId = 1000, 
                    TaxaParcelaId = 1000, 
                    NumParcelas = 1, 
                    ValorVenda = 100, 
                    Recebivel = 102.3m, 
                },
                new Venda { 
                    Id = 1001, 
                    VendedorId = 1000, 
                    MeioPagamentoId = 1001, 
                    TipoVendaId = 1001, 
                    TaxaParcelaId = 1002, 
                    NumParcelas = 3, 
                    ValorVenda = 100, 
                    Recebivel = 101.67m, 
                },
                new Venda { 
                    Id = 1002, 
                    VendedorId = 1000, 
                    MeioPagamentoId = 1001, 
                    TipoVendaId = 1002, 
                    TaxaParcelaId = 1002, 
                    NumParcelas = 4, 
                    ValorVenda = 100, 
                    Recebivel = 106.22m
                }
            );
        }
    }
}