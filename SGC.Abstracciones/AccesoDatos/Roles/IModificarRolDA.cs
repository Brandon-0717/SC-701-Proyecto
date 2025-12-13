
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface IModificarRolDA
    {
        Task<bool> Modificar(RolDA rol);
    }
}
