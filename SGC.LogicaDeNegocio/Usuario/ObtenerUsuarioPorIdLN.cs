
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class ObtenerUsuarioPorIdLN : IObtenerUsuarioPorIdLN
    {
        private readonly IObtenerUsuarioPorIdDA _obtenerUsuarioPorIdDA;
        private readonly IMapper _mapper;
        public ObtenerUsuarioPorIdLN(IObtenerUsuarioPorIdDA obtenerUsuarioPorIdDA, IMapper mapper)
        {
            _obtenerUsuarioPorIdDA = obtenerUsuarioPorIdDA;
            _mapper = mapper;
        }
        public async Task<CustomResponse<UsuarioDTO>> Obtener(string id)
        {
            var response = new CustomResponse<UsuarioDTO>();

            var usuario = await _obtenerUsuarioPorIdDA.Obtener(id);
            
            if (usuario == null)
            {
                response.EsError = true;
                response.Mensaje = "Usuario no encontrado.";
                return response;
            }

            response.Mensaje = "Usuario obtenido exitosamente.";
            response.Data = _mapper.Map<UsuarioDTO>(usuario);
            return response;
        }
    }
}
