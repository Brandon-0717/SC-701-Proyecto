using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Roles
{
    public interface IListarRolesAD
    {
        Task<List<RolDA>> Listar();
    }
}
