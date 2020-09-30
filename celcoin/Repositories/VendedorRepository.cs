using System.Collections.Generic;
using celcoin.Data;
using celcoin.Models;

namespace celcoin.Repositories
{
    public class VendedorRepository
    {
        private readonly ApplicationContext _context;
        public VendedorRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Vendedor> Get()
        {
            return _context.Vendedores;
        }
        public void Create(Vendedor vendedor)
        {
            _context.Vendedores.Add(vendedor);
            _context.SaveChanges();
        }

    }
}