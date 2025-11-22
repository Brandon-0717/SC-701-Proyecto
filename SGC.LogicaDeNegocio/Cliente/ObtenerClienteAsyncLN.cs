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
    public class ObtenerClienteAsyncLN : IObtenerClienteAsyncLN
    {
        private readonly IObtenerClienteAsyncAD _obtenerClienteAsyncAD;
        private readonly IMapper _mapper;

        public ObtenerClienteAsyncLN(IObtenerClienteAsyncAD obtenerClienteAsyncAD, IMapper mapper)
        {
            _obtenerClienteAsyncAD = obtenerClienteAsyncAD;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<ClienteDto>>> ObtenerClientesAsync()
        {
            var response = new CustomResponse<List<ClienteDto>>();

            var ClienteAD = await _obtenerClienteAsyncAD.ObtenerClientesAsync();

            if (ClienteAD == null)
            {
                response.EsError = true;
                response.Mensaje = "No hay clientes registrados en el sistema.";
                return response;
            }

            response.Data = _mapper.Map<List<ClienteDto>>(ClienteAD);
            response.Mensaje = "Clientes obtenidos con éxito.";
            return response;
        }
    }
}
