using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGC.Abstracciones.AccesoDatos.Solicitud;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Solicitud
{
    public class CambiarEstadoSolicitudDA : ICambiarEstadoSolicitudDA
    {
        private readonly Contexto _contexto;

        public CambiarEstadoSolicitudDA(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CambiarEstadoAsync(
            Guid solicitudId,
            Guid nuevoEstadoId,
            string comentario,
            string userId)
        {
            var solicitud = await _contexto.SolicitudesCredito
                .FirstOrDefaultAsync(s => s.SOLICITUDES_CREDITO_PK == solicitudId);

            if (solicitud == null) return false;

            solicitud.ESTADOS_FK_SOLICITUDES_CREDITO = nuevoEstadoId;

            if (!string.IsNullOrWhiteSpace(comentario))
            {
                solicitud.Comentario = comentario;
            }

            solicitud.ModificadoPor = userId;
            solicitud.Fecha_Modificacion = DateTime.Now;

            await _contexto.SaveChangesAsync();
            return true;
        }
    }
}

