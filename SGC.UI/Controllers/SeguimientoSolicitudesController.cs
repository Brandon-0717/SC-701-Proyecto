using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Solicitud;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.UI.Controllers
{
    [Authorize]
    public class SeguimientoSolicitudesController : Controller
    {
        private readonly IListarSolicitudesPorRolLN _listarSolicitudesLN;
        private readonly ICambiarEstadoSolicitudLN _cambiarEstadoLN;

        public SeguimientoSolicitudesController(
            IListarSolicitudesPorRolLN listarSolicitudesLN,
            ICambiarEstadoSolicitudLN cambiarEstadoLN)
        {
            _listarSolicitudesLN = listarSolicitudesLN;
            _cambiarEstadoLN = cambiarEstadoLN;
        }

        // =========================
        // LISTAR
        // =========================
        public async Task<IActionResult> Index()
        {
            var solicitudes = await _listarSolicitudesLN.ListarAsync("Todos");
            return View(solicitudes ?? new List<SolicitudCreditoDA>());
        }

        // =========================
        // ENVIAR A APROBACIÓN
        // =========================
        [HttpPost]
        public async Task<IActionResult> EnviarAprobacion([FromBody] CambiarEstadoDto dto)
        {
            if (dto == null || dto.SolicitudId == Guid.Empty)
                return BadRequest("Id inválido");

            var ok = await _cambiarEstadoLN.CambiarEstadoAsync(
                dto.SolicitudId,
                "Pendiente",
                null,
                User.Identity?.Name ?? "Sistema"
            );

            return ok ? Ok() : BadRequest("No se pudo enviar");
        }

        // =========================
        // APROBAR
        // =========================
        [HttpPost]
        public async Task<IActionResult> Aprobar([FromBody] CambiarEstadoDto dto)
        {
            if (dto == null || dto.SolicitudId == Guid.Empty)
                return BadRequest("Id inválido");

            var ok = await _cambiarEstadoLN.CambiarEstadoAsync(
                dto.SolicitudId,
                "Activo",
                null,
                User.Identity?.Name ?? "Sistema"
            );

            return ok ? Ok() : BadRequest("No se pudo aprobar");
        }

        // =========================
        // DEVOLVER
        // =========================
        [HttpPost]
        public async Task<IActionResult> Devolver([FromBody] DevolverSolicitudDto dto)
        {
            if (dto == null || dto.SolicitudId == Guid.Empty)
                return BadRequest("Datos inválidos");

            var ok = await _cambiarEstadoLN.CambiarEstadoAsync(
                dto.SolicitudId,
                "DEVOLUCION",
                dto.Comentario,
                User.Identity?.Name ?? "Sistema"
            );

            return ok ? Ok() : BadRequest("No se pudo devolver");
        }
    }

    // =========================
    // DTOs (solo para este controller)
    // =========================
    public class CambiarEstadoDto
    {
        public Guid SolicitudId { get; set; }
    }

    public class DevolverSolicitudDto
    {
        public Guid SolicitudId { get; set; }
        public string Comentario { get; set; }
    }
}
