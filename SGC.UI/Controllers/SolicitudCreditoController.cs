using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Solicitud;
using SGC.Abstracciones.Modelos.ModelosDTO;
using SGC.UI.Models;
using System.Security.Claims;
using System;
using System.IO;
using System.Linq;

namespace SGC.UI.Controllers
{
   // [Authorize(Roles = "Administrador")] // ⬅ SOLO Administrador entra aquí
    public class SolicitudCreditoController : Controller
    {
        private readonly ISolicitudCreditoLN _ln;
        private readonly IWebHostEnvironment _env;

        public SolicitudCreditoController(ISolicitudCreditoLN ln, IWebHostEnvironment env)
        {
            _ln = ln;
            _env = env;
        }

        [HttpGet]
        public IActionResult Create() => View(new SolicitudCreditoCreateVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SolicitudCreditoCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var archivosDto = new List<ArchivoCreditoDto>();
            var dateFolder = DateTime.UtcNow.ToString("yyyyMMdd");
            var batchId = Guid.NewGuid().ToString("N");
            var baseFolder = Path.Combine(_env.WebRootPath, "uploads", "creditos", dateFolder, batchId);
            Directory.CreateDirectory(baseFolder);

            foreach (var f in vm.Archivos ?? Enumerable.Empty<IFormFile>())
            {
                if (f.Length <= 0) continue;

                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(f.FileName);
                var fullPath = Path.Combine(baseFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                    await f.CopyToAsync(stream);

                var relativeUrl = $"/uploads/creditos/{dateFolder}/{batchId}/{fileName}";

                archivosDto.Add(new ArchivoCreditoDto
                {
                    NombreArchivo = fileName,
                    UrlArchivo = relativeUrl
                });
            }

            var dto = new SolicitudCreditoCrearDto
            {
                Cedula = vm.Cedula,
                MontoCredito = vm.MontoCredito,
                Comentario = vm.Comentario?.Trim(),
                CategoriaCreditoId = vm.CategoriaCreditoId,
                Archivos = archivosDto
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var creadoPor = User.Identity?.Name ?? "sistema";

            var resp = await _ln.CrearSolicitudAsync(dto, userId, creadoPor);

            if (resp.EsError)
            {
                TempData["Error"] = resp.Mensaje;
                return View(vm);
            }

            TempData["Ok"] = "Solicitud creada exitosamente.";
            return RedirectToAction("Details", new { id = resp.Data });
        }

        public IActionResult Details(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
