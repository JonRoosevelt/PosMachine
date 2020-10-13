using System;

namespace PosMachine.Exceptions
{
    public class PagamentoException : Exception
    {
        public PagamentoException()
        {

        }

        public PagamentoException(string mensagem)
            : base(mensagem)
        {

        }

        public PagamentoException(string mensagem, Exception excecaoInterna)
            : base(mensagem, excecaoInterna)
        {
            
        }
    }
}