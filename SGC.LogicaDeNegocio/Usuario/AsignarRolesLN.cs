
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class AsignarRolesLN: IAsignarRolesLN
    {
        private readonly IAsignarRolesDA _asignarRolesDA;
        public AsignarRolesLN(IAsignarRolesDA asignarRolesDA)
        {
            _asignarRolesDA = asignarRolesDA;
        }

        public async Task<CustomResponse<bool>> AsignarRoles(string identificacion,IEnumerable<string> roles)
        {
            var customResponse = new CustomResponse<bool>();

            var resultado = await _asignarRolesDA.AsignarRoles(identificacion, roles);

            if (resultado)
            {
                customResponse.Mensaje = "Roles asignados correctamente.";
            }
            else
            {
                customResponse.EsError = true;
                customResponse.Mensaje = "Error al asignar los roles.";
                customResponse.Data = false;
            }
            return customResponse;
        }
    }
}
