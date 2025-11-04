
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Roles
{
    public class EliminarRolLN : IEliminarRolLN
    {
        private readonly IEliminarRolDA _eliminarRolDA;
        private readonly IObtenerRolPorIdLN _obtenerRolPorIdLN;
        public EliminarRolLN(IEliminarRolDA eliminarRolDA, IObtenerRolPorIdLN obtenerRolPorIdLN)
        {
            _eliminarRolDA = eliminarRolDA;
            _obtenerRolPorIdLN = obtenerRolPorIdLN;
        }
        public async Task<CustomResponse<RolDTO>> Eliminar(string idRol)
        {
            var response = new CustomResponse<RolDTO>();

            var rol = await _obtenerRolPorIdLN.Obtener(idRol); //Rol es una CustomResponse

            if (rol == null)
            {
                response.EsError = true;
                response.Mensaje = rol.Mensaje;
                return response;
            }

            bool eliminado = await _eliminarRolDA.Eliminar(idRol);

            if (eliminado) 
            {
                response.Mensaje = "Rol eliminado exitosamente.";
                return response;
            }
            else
            {
                response.EsError = true;
                response.Mensaje = "No se pudo eliminar el rol.";
                return response;
            }
        }
    }
}
