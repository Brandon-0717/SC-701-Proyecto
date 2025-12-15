
using Microsoft.AspNetCore.Identity;

namespace SGC.Abstracciones.AccesoDatos.Usuario
{
    public interface ILoginDA
    {
        Task<SignInResult> Login(string email, string password);
    }
}
