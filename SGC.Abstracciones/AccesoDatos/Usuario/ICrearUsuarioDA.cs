
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface ICrearUsuarioDA
    {
        Task<bool> Crear(UsuarioDA usuario, string password);
    }
}
