using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.AccesoDatos.Solicitud
{
    public interface IActualizarEstadoSolicitudDA
    {
        Task<bool> ActualizarEstadoAsync(
            Guid solicitudId,
            Guid nuevoEstadoId,
            string comentario,
            string usuario);
    }
}