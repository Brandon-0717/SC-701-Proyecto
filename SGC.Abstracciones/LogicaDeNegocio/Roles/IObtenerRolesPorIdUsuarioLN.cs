
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Roles
{
    public interface IObtenerRolesPorIdUsuarioLN
    {
        Task<CustomResponse<List<RolDTO>>> Obtener(string idUsuario);
    }
}
