
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface IAsignarRolesDA
    {
        Task<bool> AsignarRoles(string identificacion, IEnumerable<string> roles);
    }
}
