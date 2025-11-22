using Microsoft.AspNetCore.Identity;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class CrearUsuarioDA : ICrearUsuarioDA
    {
        private readonly UserManager<UsuarioDA> _userManager;

        public CrearUsuarioDA(UserManager<UsuarioDA> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Crear(UsuarioDA usuario, string password)
        {
            var result = await _userManager.CreateAsync(usuario, password);
            return result.Succeeded;
        }
    }
}
