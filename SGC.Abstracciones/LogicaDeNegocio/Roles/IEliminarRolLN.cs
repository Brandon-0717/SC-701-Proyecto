
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Roles
{
    public interface IEliminarRolLN
    {
        Task<CustomResponse<RolDTO>> Eliminar(string idRol);
    }
}
