
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;

namespace SGC.AccesoDatos.Roles
{
    public class ValidarExistenciaRolPorIdDA : IvalidarExistenciaRolPorIdDA
    {
        private readonly Contexto _context;
        public ValidarExistenciaRolPorIdDA(Contexto context)
        {
            _context = context;
        }
        public async Task<bool> Validar(string idRol)
        {
            var resultado = await _context.Roles.FirstOrDefaultAsync(r => r.Id == idRol);
            return resultado != null;
        }
    }
}
