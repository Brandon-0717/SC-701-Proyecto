
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;

namespace SGC.AccesoDatos.Roles
{
    public class EliminarRolDA : IEliminarRolDA
    {
        private readonly Contexto _context;

        public EliminarRolDA(Contexto context)
        {
            _context = context;
        }
        public async Task<bool> Eliminar(string idRol)
        {
            return await _context.Roles.Where(r => r.Id == idRol).ExecuteDeleteAsync() > 0;
        }
    }
}
