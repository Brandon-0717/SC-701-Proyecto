
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Roles
{
    public interface IObtenerRolPorNombreLN
    {
        Task<CustomResponse<RolDTO>> Obtener(string nombreRol);
    }
}
