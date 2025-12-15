
using Microsoft.AspNetCore.Identity;
using SGC.Abstracciones.Modelos;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface ILoginLN
    {
        Task<CustomResponse<SignInResult>> Login (string email, string password);
    } 
}