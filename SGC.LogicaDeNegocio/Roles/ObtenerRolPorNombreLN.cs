using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Roles
{
    public class ObtenerRolPorNombreLN : IObtenerRolPorNombreLN
    {
        private readonly IObtenerRolPorNombreDA _obtenerRolPorNombreDA;
        private readonly IMapper _mapper;

        public ObtenerRolPorNombreLN(IObtenerRolPorNombreDA obtenerRolPorNombreDA, IMapper mapper)
        {
            _obtenerRolPorNombreDA = obtenerRolPorNombreDA;
            _mapper = mapper;
        }

        public async Task<CustomResponse<RolDTO>> Obtener(string nombreRol)
        {
            var response = new CustomResponse<RolDTO>();

            var rolDA = await _obtenerRolPorNombreDA.Obtener(nombreRol);

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
