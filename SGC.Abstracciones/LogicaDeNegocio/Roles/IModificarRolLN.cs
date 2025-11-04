
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Roles
{
    public interface IModificarRolLN
    {
        Task<CustomResponse<RolDTO>> Modificar(RolDTO rol);
    }
}
