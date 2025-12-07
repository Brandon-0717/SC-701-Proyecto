
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.AccesoDatos.Estados
{
    public interface IListarEstadosDA
    {
        Task<List<EstadoDA>> Obtener();
    }
}
