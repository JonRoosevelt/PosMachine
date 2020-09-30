using System.Collections.Generic;
using celcoin.Data;
using celcoin.Models;


namespace celcoin.Repositories
{
    public class MeioPagamentoRepository
    {
        private readonly ApplicationContext _context;
        public MeioPagamentoRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<MeioPagamento> Get()
        {
            return _context.MeiosPagamento;
        }
        public void Create(MeioPagamento meioPagamento)
        {
            _context.MeiosPagamento.Add(meioPagamento);
            _context.SaveChanges();
        }

    }
}