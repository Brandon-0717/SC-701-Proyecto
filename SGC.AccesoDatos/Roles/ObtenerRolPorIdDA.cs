
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Roles
{
    public class ObtenerRolPorIdDA : IObtenerRolPorIdDA
    {
        private readonly Contexto _context;
        public ObtenerRolPorIdDA(Contexto contexto)
        {
            _context = contexto;
        }
        public async Task<RolDA> Obtener(string idRol)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == idRol);
        }
    }
}
