using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;

namespace SGC.Abstracciones.AccesoDatos.Solicitud
{
    public interface ICambiarEstadoSolicitudDA
    {
        Task<bool> CambiarEstadoAsync(
            Guid solicitudId,
            Guid nuevoEstadoId,
            string comentario,
            string userId
        );

    }
}
