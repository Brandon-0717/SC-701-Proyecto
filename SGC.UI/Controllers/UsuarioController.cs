using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly SignInManager<UsuarioDA> _signInManager;
        private readonly UserManager<UsuarioDA> _userManager;
        private readonly IUserStore<UsuarioDA> _userStore;
        private readonly IUserEmailStore<UsuarioDA> _emailStore;
        private readonly IEmailSender _emailSender;
        //----//
        private readonly IListarUsuariosLN _listarUsuariosLN;
        private readonly IObtenerUsuarioPorIdentificacionLN _obtenerUsuarioPorIdLN;
        private readonly IEliminarUsuarioLN _eliminarUsuarioLN;
        private readonly ICrearUsuarioLN _crearUsuarioLN;
        public UsuarioController(
            IListarUsuariosLN listarUsuariosLN,
            IObtenerUsuarioPorIdentificacionLN obtenerUsuarioPorIdLN,
            IEliminarUsuarioLN eliminarUsuarioLN,
            ICrearUsuarioLN crearUsuarioLN,
            //----//
            UserManager<UsuarioDA> userManager,
            IUserStore<UsuarioDA> userStore,
            SignInManager<UsuarioDA> signInManager,
            IEmailSender emailSender
            )
        {
            _listarUsuariosLN = listarUsuariosLN;
            _obtenerUsuarioPorIdLN = obtenerUsuarioPorIdLN;
            _eliminarUsuarioLN = eliminarUsuarioLN;
            _crearUsuarioLN = crearUsuarioLN;
            //----//
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        private IUserEmailStore<UsuarioDA> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UsuarioDA>)_userStore;
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
            return RedirectToAction("Index", "Home");
        }

        //----------------------------------------------------

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

        [HttpDelete]
        public async Task<IActionResult>EliminarUsuario(string id)
        {
            var response = await _eliminarUsuarioLN.Eliminar(id);
            return Json(response);
        }

        [HttpPost] 
        public async Task<IActionResult> CrearUsuario(UsuarioDTO usuario) {
            var baseUrl = $"{Request.Scheme}://{Request.Host}"; 
            var response = await _crearUsuarioLN.Crear(usuario, baseUrl); 
            return Json(response); 
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsuarioRegistroModelDTO usuarioForm, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
                return View(usuarioForm);
            //-----------------------------------
            var emailExiste = await _userManager.FindByEmailAsync(usuarioForm.Email);
            if (emailExiste != null)
            {
                ModelState.AddModelError(string.Empty, "El correo ya está en uso.");
                return View();
            }

            var identificacionExiste = await _obtenerUsuarioPorIdLN.Obtener(usuarioForm.Identificacion);
            if (!identificacionExiste.EsError)
            {
                ModelState.AddModelError(string.Empty, "La identificación ya está en uso.");
                return View();
            }
        //-----------------------------------
        var idExistente = await _userManager.FindByNameAsync(usuarioForm.Identificacion);

            var usuario = new UsuarioDA
            {
                UserName = usuarioForm.Email,
                Email = usuarioForm.Email,
                Nombre = usuarioForm.Nombre,
                Identificacion = usuarioForm.Identificacion,
            };

            var result = await _userManager.CreateAsync(usuario, usuarioForm.Contrasenia);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
                var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var link = Url.Action("ConfirmarEmail", "Usuario",
                    new { userId = usuario.Id, code = encoded },
                    protocol: Request.Scheme);

                var safeLink = HtmlEncoder.Default.Encode(link);

                var cuerpoHtml = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background-color: #f4f6f8; padding: 40px; text-align: center;'>
                    <div style='background-color: #ffffff; max-width: 600px; margin: auto; border-radius: 8px; box-shadow: 0 4px 10px rgba(0,0,0,0.1); padding: 30px;'>
                        <h2 style='color: #333333;'>¡Bienvenido a <span style=""color:#007bff;"">SGC</span>!</h2>
                        <p style='color: #555555; font-size: 16px;'>
                            Gracias por registrarte, <strong>{usuario.Nombre}</strong>.<br>
                            Para activar tu cuenta, confirma tu correo electrónico haciendo clic en el siguiente botón:
                        </p>

                        <a href='{safeLink}'
                           style='display: inline-block; margin-top: 20px; background-color: #007bff; color: #ffffff; 
                                  text-decoration: none; padding: 12px 24px; border-radius: 5px; font-weight: bold;'>
                            Confirmar mi cuenta
                        </a>

                        <hr style='margin-top: 30px; border: none; border-top: 1px solid #eeeeee;'>
                        <p style='color: #999999; font-size: 12px;'>
                            © {DateTime.Now.Year} SGC. Todos los derechos reservados.
                        </p>
                    </div>
                </body>
                </html>";

                await _emailSender.SendEmailAsync(usuario.Email, "Confirma tu cuenta en SGC", cuerpoHtml);

                // Si el sistema requiere confirmación antes de iniciar sesión:
                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }

            // Mostrar errores de Identity
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(usuarioForm);
        }
    }
}
