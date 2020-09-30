using System.Collections.Generic;
using celcoin.Data;
using celcoin.Models;

namespace celcoin.Repositories
{
    public class VendaRepository
    {
        private readonly ApplicationContext _context;
        public VendaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Venda> Get()
        {
            return _context.Vendas;
        }

        public void Create(Venda venda)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
        }
    }
}