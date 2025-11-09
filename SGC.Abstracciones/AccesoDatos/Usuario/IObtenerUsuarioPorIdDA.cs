
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface IObtenerUsuarioPorIdDA
    {
        Task<UsuarioDA> Obtener(string id);
    }
}
