
using SGC.Abstracciones.Modelos;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface IEliminarUsuarioLN
    {
        Task<CustomResponse<bool>> Eliminar(string id);
    }
}
