using Microsoft.AspNetCore.Identity;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class EliminarUsuarioDA : IEliminarUsuarioDA
    {
        private readonly Contexto _context;
        private readonly UserManager<UsuarioDA> _userManager;
        public EliminarUsuarioDA(Contexto context, UserManager<UsuarioDA> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> Eliminar(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var resultado = await _userManager.DeleteAsync(user);

            return resultado.Succeeded;
        }
    }
}
