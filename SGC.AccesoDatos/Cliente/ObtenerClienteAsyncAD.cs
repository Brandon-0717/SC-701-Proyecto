using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.AccesoDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Cliente
{
    public class ObtenerClienteAsyncAD : IObtenerClienteAsyncAD
    {
        private Contexto _contexto;
        public ObtenerClienteAsyncAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public Task<List<ClienteDA>> ObtenerClientesAsync()
        {
           return _contexto.Clientes.ToListAsync();
        }
    }
}
