using SGC.Abstracciones.AccesoDatos.Solicitud;
using System;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Solicitud
{
    public class ActualizarEstadoSolicitudAD : IActualizarEstadoSolicitudDA
    {
        private readonly Contexto _contexto;

        public ActualizarEstadoSolicitudAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ActualizarEstadoAsync(
            Guid solicitudId,
            Guid nuevoEstadoId,
            string comentario,
            string usuario)
        {
            var solicitud = await _contexto.SolicitudesCredito
                .FindAsync(solicitudId);

            if (solicitud == null)
                return false;

            solicitud.ESTADOS_FK_SOLICITUDES_CREDITO = nuevoEstadoId;
            solicitud.Fecha_Modificacion = DateTime.Now;
            solicitud.ModificadoPor = usuario;

            // 👉 Aquí luego se conectará con GESTIONES_CREDITO / HISTÓRICO
            await _contexto.SaveChangesAsync();

            return true;
        }
    }
}
