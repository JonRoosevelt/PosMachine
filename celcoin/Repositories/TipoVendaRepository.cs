using System.Collections.Generic;
using celcoin.Data;
using celcoin.Models;

namespace celcoin.Repositories
{
    public class TipoVendaRepository
    {
        private readonly ApplicationContext _context;

        public TipoVendaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<TipoVenda> Get()
        {
            return _context.TiposVenda;
        }

        public void Create(TipoVenda tipoVenda)
        {
            _context.TiposVenda.Add(tipoVenda);
            _context.SaveChanges();
        }
    }
}