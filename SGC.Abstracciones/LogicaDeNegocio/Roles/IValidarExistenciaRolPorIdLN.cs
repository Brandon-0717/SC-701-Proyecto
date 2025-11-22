
using SGC.Abstracciones.Modelos;

namespace SGC.Abstracciones.LogicaDeNegocio.Roles
{
    public interface IValidarExistenciaRolPorIdLN
    {
        Task<CustomResponse<bool>> Validar(string idRol);
    }
}
