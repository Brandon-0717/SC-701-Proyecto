
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.AccesoDatos.Modelos;

namespace SGC.AccesoDatos
{
    public class Contexto : IdentityDbContext<UsuarioDA, RolDA, string>
    {
        
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<EstadoDA> Estados { get; set; }
        public DbSet<ClienteDA> Clientes { get; set; }
        public DbSet<GestionesCreditoDA> GestionesCredito { get; set; }
        public DbSet<ArchivoCreditoDA> ArchivosCredito { get; set; }
        public DbSet<HstGestionesCreditoDA> HstGestionesCredito { get; set; }
        public DbSet<SolicitudCreditoDA> SolicitudesCredito { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
