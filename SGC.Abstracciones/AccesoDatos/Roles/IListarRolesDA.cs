using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface IListarRolesDA
    {
        Task<List<RolDA>> Listar();
    }
}
