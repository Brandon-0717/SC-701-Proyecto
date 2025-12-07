
using SGC.Abstracciones.Modelos;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface IAsignarRolesLN
    {
        Task<CustomResponse<bool>> AsignarRoles(string identificacion, IEnumerable<string> roles);

    }
}
