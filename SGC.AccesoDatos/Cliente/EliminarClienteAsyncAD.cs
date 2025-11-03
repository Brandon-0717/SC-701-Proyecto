using SGC.Abstracciones.AccesoDatos.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Cliente
{
    public class EliminarClienteAsyncAD : IEliminarClienteAsyncAD
    {
        private Contexto _contexto;
        public EliminarClienteAsyncAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public Task<bool> EliminarClienteAsync(Guid clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
