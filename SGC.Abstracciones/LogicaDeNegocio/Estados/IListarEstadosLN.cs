
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Estados
{
    public interface IListarEstadosLN
    {
        Task<CustomResponse<List<EstadoDTO>>> Listar();
    }
}
