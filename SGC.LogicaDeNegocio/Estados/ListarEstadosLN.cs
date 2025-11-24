
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.LogicaDeNegocio.Estados;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Estados
{
    public class ListarEstadosLN : IListarEstadosLN
    {
        private readonly IListarEstadosDA _listarEstadosDA;
        private readonly IMapper _mapper;
        public ListarEstadosLN(IListarEstadosDA listarEstadosDA, IMapper mapper)
        {
            _listarEstadosDA = listarEstadosDA;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<EstadoDTO>>> Listar()
        {
            var customResponse = new CustomResponse<List<EstadoDTO>>();

            var estados = await _listarEstadosDA.Obtener();

            if (estados == null || !estados.Any())
            {
                customResponse.EsError = true;
                customResponse.Mensaje = "No se encontraron estados.";
                return customResponse;
            }

            customResponse.Mensaje = "Estados obtenidos correctamente.";
            customResponse.Data = _mapper.Map<List<EstadoDTO>>(estados);
            return customResponse;
        }
    }
}
