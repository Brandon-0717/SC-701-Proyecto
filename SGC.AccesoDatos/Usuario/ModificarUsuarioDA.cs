
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class ModificarUsuarioDA : IModificarUsuarioDA
    {
        private readonly UserManager<UsuarioDA> _userManager;
        private readonly Contexto _contexto;
        public ModificarUsuarioDA(UserManager<UsuarioDA> userManager, Contexto contexto)
        {
            _userManager = userManager;
            _contexto = contexto;
        }
        public async Task<bool> ModificarUsuario(UsuarioDA usuario)
        {

            var usuarioBD = await _contexto.Users.FirstOrDefaultAsync(u => u.Id == usuario.Id);
            if (usuarioBD == null)
            {
                return false;
            }

            usuarioBD.Nombre = usuario.Nombre;
            usuarioBD.PrimerApellido = usuario.PrimerApellido;
            usuarioBD.SegundoApellido = usuario.SegundoApellido;
            usuarioBD.FechaNacimiento = usuario.FechaNacimiento;
            usuarioBD.PhoneNumber = usuario.PhoneNumber;
            usuarioBD.Estados_FK_AspNetUsers = usuario.Estados_FK_AspNetUsers;

            var result = await _userManager.UpdateAsync(usuarioBD);
            
            return result.Succeeded;
        }

        public async Task<bool> ActualizarRoles(string identificacion, IEnumerable<string> roles)
        {
            var usuario = await _contexto.Users.FirstOrDefaultAsync(u => u.Identificacion == identificacion);
            if (usuario == null)
            {
                return false;
            }

            // Obtener roles actuales
            var rolesActuales = await _userManager.GetRolesAsync(usuario);

            // Determinar roles a eliminar y roles a agregar
            var rolesAEliminar = rolesActuales.Except(roles);
            var rolesAAgregar = roles.Except(rolesActuales);

            // Ejecutar cambios
            if (rolesAEliminar.Any())
                await _userManager.RemoveFromRolesAsync(usuario, rolesAEliminar);

            if (rolesAAgregar.Any())
                await _userManager.AddToRolesAsync(usuario, rolesAAgregar);

            return true;
        }
    }
}
