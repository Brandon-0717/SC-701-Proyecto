
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.LogicaDeNegocio.Estados;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Estados
{
    public class ObtenerEstadoPorIdLN : IObtenerEstadoPorIdLN
    {
        private readonly IObtenerEstadoPorIdDA _obtenerEstadoPorIdDA;
        private readonly IMapper _mapper;
        public ObtenerEstadoPorIdLN(IObtenerEstadoPorIdDA obtenerEstadoPorIdDA, IMapper mapper)
        {
            _obtenerEstadoPorIdDA = obtenerEstadoPorIdDA;
            _mapper = mapper;
        }
        public async Task<CustomResponse<EstadoDTO>> Obtener(string id)
        {
            var response = new CustomResponse<EstadoDTO>();

            var estado = await _obtenerEstadoPorIdDA.Obtener(Guid.Parse(id));

            if (estado == null)
            {
                response.EsError = true;
                response.Mensaje = "No se encontró el estado con el ID proporcionado.";
                return response;
            }
            response.Mensaje = "Estado obtenido exitosamente.";
            response.Data = _mapper.Map<EstadoDTO>(estado);
            return response;
        }
    }
}
