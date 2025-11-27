
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Roles
{
    public class ObtenerRolesPorIdUsuarioLN : IObtenerRolesPorIdUsuarioLN
    {
        private readonly IObtenerRolesPorIdUsuarioDA _obtenerRolesPorIdUsuarioDA;
        private readonly IMapper _mapper;
        public ObtenerRolesPorIdUsuarioLN(IObtenerRolesPorIdUsuarioDA obtenerRolesPorIdUsuarioDA, IMapper mapper)
        {
            _obtenerRolesPorIdUsuarioDA = obtenerRolesPorIdUsuarioDA;
            _mapper = mapper;
        }
        public async Task<CustomResponse<List<RolDTO>>> Obtener(string idUsuario)
        {
            var response = new CustomResponse<List<RolDTO>>();

            var rolesDA = await _obtenerRolesPorIdUsuarioDA.Obtener(idUsuario);

            if(!rolesDA.Any() || rolesDA.Count < 0)
            {
                response.EsError = true;
                response.Mensaje = "No se encontraron roles para el usuario especificado.";
                return response;
            }

            var rolesDTO = _mapper.Map<List<RolDTO>>(rolesDA);
            response.Data = rolesDTO;
            response.Mensaje = "Roles obtenidos exitosamente.";
            return response;
        }
    }
}
