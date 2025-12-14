
using Microsoft.AspNetCore.Identity;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class LoginDA : ILoginDA
    {
        private readonly SignInManager<UsuarioDA> _signInManager;

        public LoginDA(SignInManager<UsuarioDA> signInManager)
        {
            _signInManager = signInManager;
        }

        public Task<SignInResult> Login(string email, string password)
        {
            return _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
        }
    }
}
