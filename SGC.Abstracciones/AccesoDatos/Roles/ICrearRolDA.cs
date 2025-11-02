
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface ICrearRolDA
    {
        Task<bool> Crear(RolDA rol);
    }
}
