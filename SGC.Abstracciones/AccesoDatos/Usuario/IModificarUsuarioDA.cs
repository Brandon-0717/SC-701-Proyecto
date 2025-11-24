
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface IModificarUsuarioDA
    {
        Task<bool> ModificarUsuario(UsuarioDA usuario);
        Task<bool> ActualizarRoles(string identificacion, IEnumerable<string> roles);
    }
}
