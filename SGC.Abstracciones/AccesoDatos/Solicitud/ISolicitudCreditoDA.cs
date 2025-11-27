using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.AccesoDatos.Solicitud
{
    public interface ISolicitudCreditoDA
    {
        Task<Guid?> ObtenerClienteIdPorCedulaAsync(int cedula);
        Task<Guid?> ObtenerEstadoIdPorNombreAsync(string estado);
        Task<bool> ExisteSolicitudAbiertaAsync(Guid clienteId, Guid idRegistrado, Guid idDevolucion);

        Task<Guid> CrearSolicitudAsync(Guid clienteId, Guid estadoId, Guid categoriaId,
                                       string userId, decimal monto, string comentario, string creadoPor);

        Task AgregarArchivosAsync(Guid solicitudId, string userId,
                                  IEnumerable<ArchivoCreditoDto> archivos, string creadoPor);
    }
}
