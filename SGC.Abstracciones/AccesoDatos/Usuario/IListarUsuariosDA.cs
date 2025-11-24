
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface IListarUsuariosDA
    {
        Task<List<UsuarioDTO>> Obtener();
    }
}
