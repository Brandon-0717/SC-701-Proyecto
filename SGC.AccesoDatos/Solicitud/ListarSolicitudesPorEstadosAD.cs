using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Solicitud;
using SGC.Abstracciones.Modelos.ModeloDA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Solicitud
{
    public class ListarSolicitudesPorEstadosAD : IListarSolicitudesPorEstadosDA
    {
        private readonly Contexto _contexto;

        public ListarSolicitudesPorEstadosAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<SolicitudCreditoDA>> ListarPorEstadosAsync(List<Guid> estados)
        {
            return await _contexto.SolicitudesCredito
                .Where(s => estados.Contains(s.ESTADOS_FK_SOLICITUDES_CREDITO))
                .OrderByDescending(s => s.Fecha_Modificacion)
                .ToListAsync();
        }


    }
}