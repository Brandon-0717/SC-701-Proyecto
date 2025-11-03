using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.AccesoDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Cliente
{
    public class CrearClienteAsyncAD : ICrearClienteAsyncAD
    {
        private Contexto _contexto;

        public CrearClienteAsyncAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public Task<bool> CrearClienteAsync(ClienteDA cliente)
        {
            throw new NotImplementedException();
        }
    }
}
