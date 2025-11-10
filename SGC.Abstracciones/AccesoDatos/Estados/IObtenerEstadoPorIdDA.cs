
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.AccesoDatos.Estados
{
    public interface IObtenerEstadoPorIdDA
    {
        Task<EstadoDA> Obtener(Guid id);
    }
}
