using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Roles
{
    public interface IListarRolesLN
    {
        Task<CustomResponse<List<RolDTO>>> Listar();
    }
}
