
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface IObtenerUsuarioDtoPorIdLN
    {
        Task<CustomResponse<UsuarioDTO>> Obtener(string id);
    }
}
