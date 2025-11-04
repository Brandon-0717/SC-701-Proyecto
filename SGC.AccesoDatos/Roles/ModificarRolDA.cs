
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Roles
{
    public class ModificarRolDA : IModificarRolDA
    {
        private readonly Contexto _context;
        public ModificarRolDA(Contexto context)
        {
            _context = context;
        }
        public async Task<bool> Modificar(RolDA rol)
        {
            var rolExistente = await _context.Roles.FirstOrDefaultAsync(r => r.Id == rol.Id);

            if (rolExistente == null)
            {
                return false;
            }

            rolExistente.Name = rol.Name;
            rolExistente.NormalizedName = rol.NormalizedName;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
