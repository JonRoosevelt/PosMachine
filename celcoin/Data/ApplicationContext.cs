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

    }
}