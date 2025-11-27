
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface IObtenerRolPorIdDA
    {
        Task<RolDA> Obtener(string idRol);
    }
}
