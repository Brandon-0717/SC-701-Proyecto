using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Solicitud;
using SGC.Abstracciones.Modelos.ModeloDA;
using System;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Solicitud
{
    public class ObtenerSolicitudPorIdAD : IObtenerSolicitudPorIdDA
    {
        private readonly Contexto _contexto;

        public ObtenerSolicitudPorIdAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<SolicitudCreditoDA?> ObtenerPorIdAsync(Guid solicitudId)
        {
            return await _contexto.SolicitudesCredito
                .FirstOrDefaultAsync(s => s.SOLICITUDES_CREDITO_PK == solicitudId);
        }

    }
}
