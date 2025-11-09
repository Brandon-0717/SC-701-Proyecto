
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Estados
{
    public interface IObtenerEstadoPorIdLN
    {
        Task<CustomResponse<EstadoDTO>> Obtener(string id);
    }
}
