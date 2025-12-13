
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Roles
{
    public interface IObtenerRolPorIdLN
    {
        Task<CustomResponse<RolDTO>> Obtener(string idRol);
    }
}
