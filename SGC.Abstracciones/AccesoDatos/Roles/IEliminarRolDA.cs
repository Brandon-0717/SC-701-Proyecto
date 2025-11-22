
namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface IEliminarRolDA
    {
        Task<bool> Eliminar(string idRol);
    }
}
