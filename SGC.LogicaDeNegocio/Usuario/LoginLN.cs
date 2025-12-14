
using Microsoft.AspNetCore.Identity;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class LoginLN : ILoginLN
    {
        private readonly ILoginDA _loginDA;
        public LoginLN(ILoginDA loginDA)
        {
            _loginDA = loginDA;
        }
        public async Task<CustomResponse<SignInResult>> Login(string email, string password)
        {
            var response = new CustomResponse<SignInResult>();
            var resultado = await _loginDA.Login(email, password);

            if(resultado.Succeeded)
            {
                response.EsError = false;
                response.Mensaje = "Inicio de sesión exitoso.";
                response.Data = resultado;
            }
            else if (resultado.IsLockedOut)
            {
                response.EsError = true;
                response.Mensaje = "La cuenta está bloqueada debido a múltiples intentos fallidos.";
                response.Data = resultado;
            }
            else
            {
                response.EsError = true;
                response.Mensaje = "Error en el inicio de sesión. Credenciales inválidas.";
                response.Data = resultado;
            }
            return response;
        }
    }
}
