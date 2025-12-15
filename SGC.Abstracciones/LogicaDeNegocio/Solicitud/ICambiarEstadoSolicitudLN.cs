using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.LogicaDeNegocio.Solicitud
{
    public interface ICambiarEstadoSolicitudLN
    {
        Task<bool> CambiarEstadoAsync(
            Guid solicitudId,
            string nombreNuevoEstado,
            string comentario,
            string userId
        );
    }

}
