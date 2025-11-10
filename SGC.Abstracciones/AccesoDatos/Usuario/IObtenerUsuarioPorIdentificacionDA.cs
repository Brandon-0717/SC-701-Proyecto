
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface IObtenerUsuarioPorIdentificacionDA
    {
        Task<UsuarioDA> Obtener(string id);
    }
}
