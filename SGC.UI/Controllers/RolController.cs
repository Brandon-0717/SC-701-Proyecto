using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Roles;

namespace SGC.UI.Controllers
{
    public class RolController : Controller
    {
        private readonly IListarRolesLN _listarRolesLN;

        public RolController(IListarRolesLN listarRolesLN)
        {
            _listarRolesLN = listarRolesLN;
        }

        //----------------------------------------------------

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListarRoles()
        {
            var response = await _listarRolesLN.Listar();
            return Json(response);
        }
    }
}
