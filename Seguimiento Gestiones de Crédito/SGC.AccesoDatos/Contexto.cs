
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGC.AccesoDatos.Modelos;

namespace SGC.AccesoDatos
{
    public class Contexto : IdentityDbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<ClienteDA> Clientes { get; set; }
    }
}
