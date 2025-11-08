
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface IListarUsuariosDA
    {
        Task<List<UsuarioDA>> Obtener();
    }
}
