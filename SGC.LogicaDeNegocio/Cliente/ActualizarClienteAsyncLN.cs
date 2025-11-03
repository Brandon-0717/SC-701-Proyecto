using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.Abstracciones.LogicaDeNegocio.Cliente;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;
using SGC.AccesoDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Cliente
{
    public class ActualizarClienteAsyncLN : IActualizarClienteAsyncLN
    {
        private readonly IActualizarClienteAsyncAD _actualizarClienteAsyncAD;
        private readonly IMapper _mapper;

        public ActualizarClienteAsyncLN(IActualizarClienteAsyncAD actualizarClienteAsyncAD, IMapper mapper)
        {
            _actualizarClienteAsyncAD = actualizarClienteAsyncAD;
            _mapper = mapper;
        }

        public async Task<CustomResponse<ClienteDto>> ActualizarClienteAsync(ClienteDto clienteDto)
        {
            var response = new CustomResponse<ClienteDto>();

            try
            {
                if (clienteDto.CLIENTES_PK == Guid.Empty)
                    throw new ArgumentException("El identificador del cliente es obligatorio.");

                var clienteDA = _mapper.Map<ClienteDA>(clienteDto);

                var actualizado = await _actualizarClienteAsyncAD.ActualizarClienteAsync(clienteDA);

                if (actualizado)
                {
                    response.EsError = false;
                    response.Mensaje = "Cliente actualizado correctamente.";
                    response.Data = clienteDto;
                }
                else
                {
                    response.EsError = true;
                    response.Mensaje = "No se pudo actualizar el cliente.";
                }
            }
            catch (Exception ex)
            {
                response.EsError = true;
                response.Mensaje = $"Error al actualizar el cliente: {ex.Message}";
            }

            return response;
        }
    }
}
//