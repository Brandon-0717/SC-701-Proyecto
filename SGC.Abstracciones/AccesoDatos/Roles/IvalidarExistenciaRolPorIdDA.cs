
namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface IvalidarExistenciaRolPorIdDA
    {
        Task<bool> Validar(string idRol);
    }
}
