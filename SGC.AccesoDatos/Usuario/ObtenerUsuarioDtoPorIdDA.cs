
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.AccesoDatos.Usuario
{
    public class ObtenerUsuarioDtoPorIdDA : IObtenerUsuarioDtoPorIdDA
    {
        private readonly Contexto _contexto;
        public ObtenerUsuarioDtoPorIdDA(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<UsuarioDTO> Obtener(string id)
        {
            var usuario = await(
                from u in _contexto.Users
                where u.Id == id
                join e in _contexto.Estados on u.Estados_FK_AspNetUsers equals e.ESTADOS_PK into estadoJoin
                from estado in estadoJoin.DefaultIfEmpty()
                join ur in _contexto.UserRoles on u.Id equals ur.UserId into rolesJoin
                from ur in rolesJoin.DefaultIfEmpty()
                join r in _contexto.Roles on ur.RoleId equals r.Id into rolJoin
                from rol in rolJoin.DefaultIfEmpty()

                group new { u, estado, rol } by u.Id into g

                select new UsuarioDTO
                {
                    Id = g.Key,
                    Email = g.First().u.Email,
                    Telefono = g.First().u.PhoneNumber,
                    UserName = g.First().u.UserName,
                    Nombre = g.First().u.Nombre,
                    PrimerApellido = g.First().u.PrimerApellido,
                    SegundoApellido = g.First().u.SegundoApellido,
                    Identificacion = g.First().u.Identificacion,
                    FechaNacimiento = g.First().u.FechaNacimiento,
                    FotoPerfilUrl = g.First().u.FotoPerfilUrl,
                    Estados_FK_AspNetUsers = g.First().u.Estados_FK_AspNetUsers,
                    NombreEstado = g.First().estado != null ? g.First().estado.Nombre_Estado : null,
                    EmailConfirmed = g.First().u.EmailConfirmed,
                    Roles = g
                        .Where(x => x.rol != null)
                        .Select(x => new RolDTO
                        {
                            Id = x.rol.Id,
                            Name = x.rol.Name,
                            NormalizedName = x.rol.NormalizedName
                        })
                        .Distinct()
                        .ToList()
                }
            ).FirstOrDefaultAsync();

            return usuario;
        }
    }
}
