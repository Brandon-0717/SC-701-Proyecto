
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Roles
{
    public class EliminarRolLN : IEliminarRolLN
    {
        private readonly IEliminarRolDA _eliminarRolDA;
        private readonly IValidarExistenciaRolPorIdLN _validarExistenciaRolPorIdLN;
        public EliminarRolLN(IEliminarRolDA eliminarRolDA, IValidarExistenciaRolPorIdLN validarExistenciaRolPorIdLN)
        {
            _eliminarRolDA = eliminarRolDA;
            _validarExistenciaRolPorIdLN = validarExistenciaRolPorIdLN;
        }
        public async Task<CustomResponse<RolDTO>> Eliminar(string idRol)
        {
            var response = new CustomResponse<RolDTO>();

            var existe = await _validarExistenciaRolPorIdLN.Validar(idRol);

            if (existe.EsError)
            {
                response.EsError = true;
                response.Mensaje = existe.Mensaje;
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
