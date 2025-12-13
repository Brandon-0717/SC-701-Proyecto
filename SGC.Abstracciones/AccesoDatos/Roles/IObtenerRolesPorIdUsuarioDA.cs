
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface IObtenerRolesPorIdUsuarioDA
    {
        Task<List<RolDA>> Obtener(string idUsuario);
    }
}
