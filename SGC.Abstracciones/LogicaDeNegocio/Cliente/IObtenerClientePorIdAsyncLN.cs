using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.LogicaDeNegocio.Cliente
{
    public interface IObtenerClientePorIdAsyncLN
    {
        Task<CustomResponse<ClienteDto>> ObtenerClientePorIdAsync(string clienteId);
    }
}
