
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class AsignarRolesDA : IAsignarRolesDA
    {
        private readonly UserManager<UsuarioDA> _userManager;
        private readonly Contexto _contexto;
        public AsignarRolesDA(UserManager<UsuarioDA> userManager, Contexto contexto)
        {
            _userManager = userManager;
            _contexto = contexto;
        }
        public async Task<bool> AsignarRoles(string identificacion,IEnumerable<string> roles)
        {
            var usuario = await _contexto.Users.FirstOrDefaultAsync(u => u.Identificacion == identificacion);
            if (usuario == null) {
                return false;
            }

            var result = await _userManager.AddToRolesAsync(usuario, roles);
            return result.Succeeded;
        }
    }
}
