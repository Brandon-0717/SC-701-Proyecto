using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.LogicaDeNegocio.Solicitud;
using System;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Solicitud
{
    public class CambiarEstadoSolicitudLN : ICambiarEstadoSolicitudLN
    {
        // 👉 Namespace COMPLETO (sin using)
        private readonly SGC.Abstracciones.AccesoDatos.Solicitud.ICambiarEstadoSolicitudDA _da;
        private readonly IObtenerEstadoPorNombreDA _estadoDA;

        public CambiarEstadoSolicitudLN(
            SGC.Abstracciones.AccesoDatos.Solicitud.ICambiarEstadoSolicitudDA da,
            IObtenerEstadoPorNombreDA estadoDA)
        {
            _da = da;
            _estadoDA = estadoDA;
        }

        public async Task<bool> CambiarEstadoAsync(
            Guid solicitudId,
            string nombreNuevoEstado,
            string comentario,
            string userId)
        {
            var estado = await _estadoDA.ObtenerPorNombreAsync(nombreNuevoEstado);
            if (estado == null) return false;

            return await _da.CambiarEstadoAsync(
                solicitudId,
                estado.ESTADOS_PK,
                comentario,
                userId
            );
        }
    }
}
