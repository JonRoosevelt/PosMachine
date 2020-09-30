using System.Collections.Generic;
using celcoin.Data;
using celcoin.Models;

namespace celcoin.Repositories
{
    public class TaxasRepository
    {
        private readonly ApplicationContext _context;
        public TaxasRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Taxa> Get()
        {
            return _context.Taxas;
        }

        public void Create(Taxa taxa)
        {
            _context.Taxas.Add(taxa);
            _context.SaveChanges();
        }
    }
}