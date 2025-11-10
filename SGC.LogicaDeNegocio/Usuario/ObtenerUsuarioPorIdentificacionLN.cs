
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class ObtenerUsuarioPorIdentificacionLN : IObtenerUsuarioPorIdentificacionLN
    {
        private readonly IObtenerUsuarioPorIdentificacionDA _obtenerUsuarioPorIdDA;
        private readonly IMapper _mapper;
        public ObtenerUsuarioPorIdentificacionLN(IObtenerUsuarioPorIdentificacionDA obtenerUsuarioPorIdDA, IMapper mapper)
        {
            _obtenerUsuarioPorIdDA = obtenerUsuarioPorIdDA;
            _mapper = mapper;
        }
        public async Task<CustomResponse<UsuarioDTO>> Obtener(string id)
        {
            var response = new CustomResponse<UsuarioDTO>();

            var usuarioDA = await _obtenerUsuarioPorIdDA.Obtener(id);

            if (usuarioDA == null)
            {
                response.EsError = true;
                response.Mensaje = "Usuario no encontrado.";
                return response;
            }

            response.Data = _mapper.Map<UsuarioDTO>(usuarioDA);
            response.Mensaje = "Usuario obtenido con éxito.";
            return response;
        }
    }
}
