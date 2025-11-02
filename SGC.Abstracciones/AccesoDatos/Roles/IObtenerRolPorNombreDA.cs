
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface IObtenerRolPorNombreDA
    {
        Task<RolDA> Obtener(string nombreRol);
    }
}
