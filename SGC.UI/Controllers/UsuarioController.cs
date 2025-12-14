using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly SignInManager<UsuarioDA> _signInManager;
        private readonly UserManager<UsuarioDA> _userManager;
        //----//
        private readonly IListarUsuariosLN _listarUsuariosLN;
        private readonly IObtenerUsuarioPorIdentificacionLN _obtenerUsuarioPorIdentificacionLN;
        private readonly IEliminarUsuarioLN _eliminarUsuarioLN;
        private readonly ICrearUsuarioLN _crearUsuarioLN;
        private readonly IModificarUsuarioLN _modificarUsuarioLN;
        private readonly IObtenerUsuarioPorIdLN _obtenerUsuarioPorIdLN;
        private readonly IRegistrarUsuarioLN _registrarUsuarioLN;
        private readonly IObtenerUsuarioDtoPorIdLN _obtenerUsuarioDtoPorIdLN;
        private readonly ILoginLN _loginLN;


        public UsuarioController(
            IListarUsuariosLN listarUsuariosLN,
            IObtenerUsuarioPorIdentificacionLN obtenerUsuarioPorIdentificacionLN,
            IEliminarUsuarioLN eliminarUsuarioLN,
            ICrearUsuarioLN crearUsuarioLN,
            IModificarUsuarioLN modificarUsuarioLN,
            IObtenerUsuarioPorIdLN obtenerUsuarioPorIdLN,
            IRegistrarUsuarioLN registrarUsuarioLN,
            IObtenerUsuarioDtoPorIdLN obtenerUsuarioDtoPorIdLN,
            ILoginLN loginLN,
            //----//
            SignInManager<UsuarioDA> signInManager,
            UserManager<UsuarioDA> userManager

            )
        {
            _listarUsuariosLN = listarUsuariosLN;
            _obtenerUsuarioPorIdentificacionLN = obtenerUsuarioPorIdentificacionLN;
            _eliminarUsuarioLN = eliminarUsuarioLN;
            _crearUsuarioLN = crearUsuarioLN;
            _modificarUsuarioLN = modificarUsuarioLN;
            _obtenerUsuarioPorIdLN = obtenerUsuarioPorIdLN;
            _registrarUsuarioLN = registrarUsuarioLN;
            _obtenerUsuarioDtoPorIdLN = obtenerUsuarioDtoPorIdLN;
            _loginLN = loginLN;
            //----//
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> ConfirmarEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View("EmailConfirmado");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        //----------------------------------------------------
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var response = await _listarUsuariosLN.Obtener();
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarioPorId(string id)
        {
            var response = await _obtenerUsuarioPorIdLN.Obtener(id);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarioDtoPorId(string id)
        {
            var response = await _obtenerUsuarioDtoPorIdLN.Obtener(id);
            return Json(response);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            var response = await _eliminarUsuarioLN.Eliminar(id);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(UsuarioDTO usuario)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var response = await _crearUsuarioLN.Crear(usuario, baseUrl);
            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> ModificarUsuario(UsuarioDTO usuario)
        {
            var response = await _modificarUsuarioLN.ModificarUsuario(usuario);
            return Json(response);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioRegistroModelDTO usuarioForm)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var response = await _registrarUsuarioLN.Registrar(usuarioForm, baseUrl);
            return Json(response);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
             var response = await _loginLN.Login(email, password);
             return Json(response);
        }
    }
}
