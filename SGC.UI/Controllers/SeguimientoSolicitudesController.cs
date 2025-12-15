using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Solicitud;
using SGC.Abstracciones.Modelos.ModeloDA;
using System.Security.Claims;

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
        // LISTAR SEGÚN ROL
        // =========================
        public async Task<IActionResult> Index()
        {
            var rol = User.FindFirstValue(ClaimTypes.Role) ?? "Cliente";
            var solicitudes = await _listarSolicitudesLN.ListarAsync(rol);
            return View(solicitudes ?? new List<SolicitudCreditoDA>());
        }

        // =========================
        // ENVIAR A APROBACIÓN
        // SOLO: Analista / Administrador
        // =========================
        [HttpPost]
        [Authorize(Roles = "Analista,Administrador")]
        public async Task<IActionResult> EnviarAprobacion([FromBody] CambiarEstadoDto dto)
        {
            if (dto == null || dto.SolicitudId == Guid.Empty)
                return BadRequest();

            var ok = await _cambiarEstadoLN.CambiarEstadoAsync(
                dto.SolicitudId,
                "Pendiente",
                null,
                User.Identity?.Name ?? "Sistema"
            );

            return ok ? Ok() : BadRequest();
        }

        // =========================
        // APROBAR
        // SOLO: Gestor / Administrador
        // =========================
        [HttpPost]
        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Aprobar([FromBody] CambiarEstadoDto dto)
        {
            if (dto == null || dto.SolicitudId == Guid.Empty)
                return BadRequest();

            var ok = await _cambiarEstadoLN.CambiarEstadoAsync(
                dto.SolicitudId,
                "Activo",
                null,
                User.Identity?.Name ?? "Sistema"
            );

            return ok ? Ok() : BadRequest();
        }

        // =========================
        // DEVOLVER
        // SOLO: Gestor / Administrador
        // =========================
        [HttpPost]
        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Devolver([FromBody] DevolverSolicitudDto dto)
        {
            if (dto == null || dto.SolicitudId == Guid.Empty)
                return BadRequest();

            var ok = await _cambiarEstadoLN.CambiarEstadoAsync(
                dto.SolicitudId,
                "DEVOLUCION",
                dto.Comentario,
                User.Identity?.Name ?? "Sistema"
            );

            return ok ? Ok() : BadRequest();
        }
    }

    // =========================
    // DTOs
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
