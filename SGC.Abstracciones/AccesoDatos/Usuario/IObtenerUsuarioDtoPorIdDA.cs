
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface IObtenerUsuarioDtoPorIdDA
    {
        Task<UsuarioDTO> Obtener(string id);
    }
}
