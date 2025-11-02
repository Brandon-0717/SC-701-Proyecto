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

        public RolController(
            IListarRolesLN listarRolesLN, 
            ICrearRolLN crearRolLN, 
            IObtenerRolPorNombreLN obtenerRolPorNombreLN
            )
        {
            _listarRolesLN = listarRolesLN;
            _crearRolLN = crearRolLN;
            _obtenerRolPorNombreLN = obtenerRolPorNombreLN;
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
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(RolDTO Rol)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _crearRolLN.Crear(Rol);

            if (response.EsError)
                return BadRequest(response);

            return Ok(response); 
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRolPorNombre(string NombreRol)
        {
            var response = await _obtenerRolPorNombreLN.Obtener(NombreRol);
            
            if (response.EsError)
                return NotFound(response);

            return Ok(response);
        }
            
    }
}
