
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface IEliminarUsuarioDA
    {
        Task<bool> Eliminar(string id);
    }
}
