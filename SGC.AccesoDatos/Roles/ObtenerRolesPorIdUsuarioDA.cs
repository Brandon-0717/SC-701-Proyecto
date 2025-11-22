
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Roles
{
    public class ObtenerRolesPorIdUsuarioDA : IObtenerRolesPorIdUsuarioDA
    {
        private readonly Contexto _context;
        public ObtenerRolesPorIdUsuarioDA(Contexto context)
        {
            _context = context;
        }
        public async Task<List<RolDA>> Obtener(string idUsuario)
        {
            return await (from rol in _context.Roles
                          join userRol in _context.UserRoles on rol.Id equals userRol.RoleId
                          join user in _context.Users on userRol.UserId equals user.Id
                          where user.Id == idUsuario
                          select rol).ToListAsync();
        }
    }
}
