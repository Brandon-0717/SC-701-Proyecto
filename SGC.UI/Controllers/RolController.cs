using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.UI.Controllers
{
    public class RolController : Controller
    {
        private readonly IListarRolesLN _listarRolesLN;
        private readonly ICrearRolLN _crearRolLN;
        private readonly IObtenerRolPorNombreLN _obtenerRolPorNombreLN;
        private readonly IObtenerRolPorIdLN _obtenerRolPorIdLN;
        private readonly IEliminarRolLN _eliminarRolLN;
        private readonly IModificarRolLN _modificarRolLN;

        public RolController(
            IListarRolesLN listarRolesLN,
            ICrearRolLN crearRolLN,
            IObtenerRolPorNombreLN obtenerRolPorNombreLN,
            IObtenerRolPorIdLN obtenerRolPorIdLN,
            IEliminarRolLN eliminarRolLN,
            IModificarRolLN modificarRolLN
            )
        {
            _listarRolesLN = listarRolesLN;
            _crearRolLN = crearRolLN;
            _obtenerRolPorNombreLN = obtenerRolPorNombreLN;
            _obtenerRolPorIdLN = obtenerRolPorIdLN;
            _eliminarRolLN = eliminarRolLN;
            _modificarRolLN = modificarRolLN;
        }

        //----------------------------------------------------

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarRoles()
        {
            var response = await _listarRolesLN.Listar();
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(RolDTO Rol)
        {
            var response = await _crearRolLN.Crear(Rol);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRolPorNombre(string NombreRol)
        {
            var response = await _obtenerRolPorNombreLN.Obtener(NombreRol);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRolPorId(string IdRol)
        {
            var response = await _obtenerRolPorIdLN.Obtener(IdRol);
            return Json(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarRol(string IdRol)
        {
            var response = await _eliminarRolLN.Eliminar(IdRol);
            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> ModificarRol(RolDTO Rol)
        {
            var response = await _modificarRolLN.Modificar(Rol);
            return Json(response);
        }
    }
}
