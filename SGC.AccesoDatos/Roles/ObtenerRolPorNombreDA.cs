
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Roles
{
    public class ObtenerRolPorNombreDA : IObtenerRolPorNombreDA
    {
        private readonly Contexto _context;

        public ObtenerRolPorNombreDA(Contexto context)
        {
            _context = context;
        }

        public async Task<RolDA> Obtener(string nombreRol)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == nombreRol); //Esto para simplificar ya que esta en MAYus
        }
    }
}
