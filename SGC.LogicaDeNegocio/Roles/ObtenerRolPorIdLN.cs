
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Roles
{
    public class ObtenerRolPorIdLN : IObtenerRolPorIdLN
    {
        private readonly IObtenerRolPorIdDA _obtenerRolPorIdDA;
        private readonly IMapper _mapper;
        public ObtenerRolPorIdLN(IObtenerRolPorIdDA obtenerRolPorIdDA, IMapper mapper)
        {
            _obtenerRolPorIdDA = obtenerRolPorIdDA;
            _mapper = mapper;
        }
        public async Task<CustomResponse<RolDTO>> Obtener(string idRol)
        {
            var response = new CustomResponse<RolDTO>();

            var rolDA = await _obtenerRolPorIdDA.Obtener(idRol);

            if (rolDA == null)
            {
                response.EsError = true;
                response.Mensaje = "Rol no encontrado.";
                return response;
            }

            response.Data = _mapper.Map<RolDTO>(rolDA);
            response.Mensaje = "Rol obtenido con éxito.";
            return response;
        }
    }
}
