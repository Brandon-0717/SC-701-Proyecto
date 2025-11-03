using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Cliente;
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
    public class ObtenerClientePorIdAsyncLN : IObtenerClientePorIdAsyncLN
    {
        private readonly IMapper _mapper;
        private readonly IObtenerClientePorIdAsyncAD _obtenerClientePorIdAsyncAD;

        public ObtenerClientePorIdAsyncLN(IMapper mapper, IObtenerClientePorIdAsyncAD obtenerClientePorIdAsyncAD)
        {
            _mapper = mapper;
            _obtenerClientePorIdAsyncAD = obtenerClientePorIdAsyncAD;
        }


        public async Task<CustomResponse<ClienteDto>> ObtenerClientePorIdAsync(string clienteId)
        {
            var response = new CustomResponse<ClienteDto>();

            // Validar que el string no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(clienteId))
            {
                response.EsError = true;
                response.Mensaje = "El identificador del cliente no puede estar vacío.";
                return response;
            }

            // Intentar convertir el string a Guid
            if (!Guid.TryParse(clienteId, out Guid clienteGuid))
            {
                response.EsError = true;
                response.Mensaje = "El identificador del cliente no tiene un formato válido.";
                return response;
            }

            // Llamar al método de acceso a datos con el Guid ya convertido
            var clienteAD = await _obtenerClientePorIdAsyncAD.ObtenerClientePorIdAsync(clienteGuid);

            if (clienteAD == null)
            {
                response.EsError = true;
                response.Mensaje = "No se encontró un cliente con el identificador proporcionado.";
                return response;
            }

            // Mapear el resultado
            response.Data = _mapper.Map<ClienteDto>(clienteAD);
            response.Mensaje = "Cliente obtenido con éxito.";

            return response;
        }
    }
}
