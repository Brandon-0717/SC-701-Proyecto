using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Solicitud;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.AccesoDatos.Solicitud
{
    public class SolicitudCreditoAD : ISolicitudCreditoDA
    {
        private readonly Contexto _ctx;
        public SolicitudCreditoAD(Contexto ctx) => _ctx = ctx;

        public async Task<Guid?> ObtenerClienteIdPorCedulaAsync(int cedula)
        {
            return await _ctx.Clientes
                .Where(c => c.Cedula == cedula)
                .Select(c => (Guid?)c.CLIENTES_PK)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid?> ObtenerEstadoIdPorNombreAsync(string estado)
        {
            return await _ctx.Estados
                .Where(e => e.Nombre_Estado == estado)
                .Select(e => (Guid?)e.ESTADOS_PK)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExisteSolicitudAbiertaAsync(Guid clienteId, Guid idRegistrado, Guid idDevolucion)
        {
            return await _ctx.SolicitudesCredito.AnyAsync(s =>
                s.CLIENTES_FK_SOLICITUES_CREDITO == clienteId &&
                (s.ESTADOS_FK_SOLICITUDES_CREDITO == idRegistrado ||
                 s.ESTADOS_FK_SOLICITUDES_CREDITO == idDevolucion));
        }

        public async Task<Guid> CrearSolicitudAsync(Guid clienteId, Guid estadoId, Guid categoriaId,
                                                    string userId, decimal monto, string comentario, string creadoPor)
        {
            var entity = new SolicitudCreditoDA
            {
                SOLICITUDES_CREDITO_PK = Guid.NewGuid(),
                CLIENTES_FK_SOLICITUES_CREDITO = clienteId,
                ESTADOS_FK_SOLICITUDES_CREDITO = estadoId,
                CATEGORIAS_CREDITO_FK_SOLICITUDES_CREDITO = categoriaId,
                UserId_FK_SOLICITUES_CREDITO = userId,
                Monto_Credito = monto,
                Comentario = comentario,
                CreadoPor = creadoPor,
                Fecha_Modificacion = DateTime.UtcNow
            };

            _ctx.SolicitudesCredito.Add(entity);
            await _ctx.SaveChangesAsync();

            return entity.SOLICITUDES_CREDITO_PK;
        }

        public async Task AgregarArchivosAsync(Guid solicitudId, string userId,
                                               IEnumerable<ArchivoCreditoDto> archivos, string creadoPor)
        {
            foreach (var a in archivos)
            {
                _ctx.ArchivosCredito.Add(new ArchivoCreditoDA
                {
                    ARCHIVOS_CREDITO_PK = Guid.NewGuid(),
                    SOLICITUDES_CREDITO_FK_ARCHIVOS_CREDITO = solicitudId,
                    UserId_FK_ARCHIVOS_CREDITO = userId,
                    Nombre_Archivo = a.NombreArchivo,
                    Url_Archivo = a.UrlArchivo,
                    Fecha_Creacion = DateTime.UtcNow,
                    CreadoPor = creadoPor
                });
            }

            await _ctx.SaveChangesAsync();
        }
    }
}
