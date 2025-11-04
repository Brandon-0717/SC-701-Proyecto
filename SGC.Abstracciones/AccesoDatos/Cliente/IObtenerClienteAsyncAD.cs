using SGC.AccesoDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.AccesoDatos.Cliente
{
    public interface IObtenerClienteAsyncAD
    {
        Task<List<ClienteDA>> ObtenerClientesAsync();
    }
}
