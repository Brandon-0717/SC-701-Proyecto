
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.UI.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UsuarioDA> _signInManager;
        private readonly UserManager<UsuarioDA> _userManager;
        private readonly IUserStore<UsuarioDA> _userStore;
        private readonly IUserEmailStore<UsuarioDA> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<UsuarioDA> userManager,
            IUserStore<UsuarioDA> userStore,
            SignInManager<UsuarioDA> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // Campos personalizados
            [Required]
            [MaxLength(256)]
            [Display(Name = "Nombre")]
            public string Nombre { get; set; }

            [Required]
            [MaxLength(256)]
            [Display(Name = "Primer Apellido")]
            public string PrimerApellido { get; set; }

            [Required]
            [MaxLength(256)]
            [Display(Name = "Segundo Apellido")]
            public string SegundoApellido { get; set; }

            [Required]
            [MaxLength(256)]
            [Display(Name = "Identificación")]
            public string Identificacion { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Fecha de Nacimiento")]
            public DateTime FechaNacimiento { get; set; }

            [Required]
            [Display(Name = "Estado")]
            public Guid Estados_FK_AspNetUsers { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                user.Nombre = Input.Nombre;
                user.PrimerApellido = Input.PrimerApellido;
                user.SegundoApellido = Input.SegundoApellido;
                user.Identificacion = Input.Identificacion;
                user.FechaNacimiento = Input.FechaNacimiento;
                user.Estados_FK_AspNetUsers = Input.Estados_FK_AspNetUsers;

                if (string.IsNullOrWhiteSpace(user.FotoPerfilUrl))
                    user.FotoPerfilUrl = "/img/avatars/default.png";


                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private UsuarioDA CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UsuarioDA>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UsuarioDA)}'. " +
                    $"Ensure that '{nameof(UsuarioDA)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<UsuarioDA> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UsuarioDA>)_userStore;
        }
    }
}