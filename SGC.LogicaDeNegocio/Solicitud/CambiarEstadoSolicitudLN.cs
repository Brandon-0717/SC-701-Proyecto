using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.LogicaDeNegocio.Bitacora;
using SGC.Abstracciones.LogicaDeNegocio.Solicitud;
using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Solicitud
{
    public class CambiarEstadoSolicitudLN : ICambiarEstadoSolicitudLN
    {
        private readonly SGC.Abstracciones.AccesoDatos.Solicitud.ICambiarEstadoSolicitudDA _da;
        private readonly IObtenerEstadoPorNombreDA _estadoDA;
        private readonly ICrearBitacoraLN _crearBitacoraLN;

        public CambiarEstadoSolicitudLN(
            SGC.Abstracciones.AccesoDatos.Solicitud.ICambiarEstadoSolicitudDA da,
            IObtenerEstadoPorNombreDA estadoDA,
            ICrearBitacoraLN crearBitacoraLN)
        {
            _da = da;
            _estadoDA = estadoDA;
            _crearBitacoraLN = crearBitacoraLN;
        }

        public async Task<bool> CambiarEstadoAsync(
     Guid solicitudId,
     string nombreNuevoEstado,
     string comentario,
     string userId)
        {
            var estadoNuevo = await _estadoDA.ObtenerPorNombreAsync(nombreNuevoEstado);
            if (estadoNuevo == null)
                return false;

            var resultado = await _da.CambiarEstadoAsync(
                solicitudId,
                estadoNuevo.ESTADOS_PK,
                comentario,
                userId
            );

            if (!resultado)
                return false;

            


            var bitacoraDto = new BitacoraDto
            {
                Gestion = 0, // ✅ INT válido (ajusta si luego tienes el ID real)
                Accion = "CAMBIO_ESTADO",
                Comentario = $"Estado cambiado a {estadoNuevo.Nombre_Estado}. {comentario}",
                Usuario = userId,
                Fecha = DateTime.UtcNow
            };

            // ✅ Método void, sin await
            _crearBitacoraLN.CrearBitacora(bitacoraDto);

            return true;
        }

        
    }
}