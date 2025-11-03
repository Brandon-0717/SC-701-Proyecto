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
    public class ObtenerClientePorIdAsyncAD : IObtenerClientePorIdAsyncAD
    {
        private Contexto _contexto;
        public ObtenerClientePorIdAsyncAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<ClienteDA> ObtenerClientePorIdAsync(Guid clienteId)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(c  => c.CLIENTES_PK == clienteId);
        }
    }
}
