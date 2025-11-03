using SGC.Abstracciones.LogicaDeNegocio.Cliente;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Cliente
{
    public class EliminarClienteAsyncLN : IEliminarClienteAsyncLN
    {
        public Task<CustomResponse<ClienteDto>> EliminarClienteAsync(string clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
