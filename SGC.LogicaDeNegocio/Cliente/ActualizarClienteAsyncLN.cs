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
    public class ActualizarClienteAsyncLN : IActualizarClienteAsyncLN
    {
        public Task<CustomResponse<ClienteDto>> ActualizarClienteAsync(ClienteDto cliente)
        {
            throw new NotImplementedException();
        }
    }
}
