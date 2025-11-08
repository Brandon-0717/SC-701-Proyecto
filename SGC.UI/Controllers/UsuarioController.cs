using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;

namespace SGC.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IListarUsuariosLN _listarUsuariosLN;
        public UsuarioController(IListarUsuariosLN listarUsuariosLN)
        {
            _listarUsuariosLN = listarUsuariosLN;
        }

        //----------------------------------------------------

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerUsuarios()
        {
            var response = await _listarUsuariosLN.Obtener();
            return Json(response);
        }
    }
}
