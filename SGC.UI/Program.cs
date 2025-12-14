using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Cliente;
using SGC.Abstracciones.LogicaDeNegocio.Estados;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.AccesoDatos;
using SGC.AccesoDatos.Cliente;
using SGC.AccesoDatos.Estados;
using SGC.AccesoDatos.Roles;
using SGC.AccesoDatos.Usuario;
using SGC.LogicaDeNegocio;
using SGC.LogicaDeNegocio.Cliente;
using SGC.LogicaDeNegocio.Estados;
using SGC.LogicaDeNegocio.Mapper;
using SGC.LogicaDeNegocio.Roles;
using SGC.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.AccesoDatos.Solicitud;
using SGC.Abstracciones.LogicaDeNegocio.Solicitud;
using SGC.AccesoDatos.Solicitud;
using SGC.LogicaDeNegocio.Solicitud;
using SGC.Abstracciones.LogicaDeNegocio.Bitacora;
using SGC.LogicaDeNegocio.Bitacora;
using SGC.AccesoDatos.Bitacora;
using SGC.Abstracciones.AccesoDatos.Bitacora;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

#region INYECCION DEPENDENCIAS
//Roles
builder.Services.AddTransient<IListarRolesDA, ListarRolesDA>();
builder.Services.AddTransient<IListarRolesLN, ListarRolesLN>();
builder.Services.AddTransient<IObtenerRolPorNombreDA, ObtenerRolPorNombreDA>();
builder.Services.AddTransient<IObtenerRolPorNombreLN, ObtenerRolPorNombreLN>();
builder.Services.AddTransient<ICrearRolDA, CrearRolDA>();
builder.Services.AddTransient<ICrearRolLN, CrearRolLN>();
builder.Services.AddTransient<IEliminarRolDA, EliminarRolDA>();
builder.Services.AddTransient<IEliminarRolLN, EliminarRolLN>();
builder.Services.AddTransient<IObtenerRolPorIdDA, ObtenerRolPorIdDA>();
builder.Services.AddTransient<IObtenerRolPorIdLN, ObtenerRolPorIdLN>();
builder.Services.AddTransient<IModificarRolDA, ModificarRolDA>();
builder.Services.AddTransient<IModificarRolLN, ModificarRolLN>();
builder.Services.AddTransient<IvalidarExistenciaRolPorIdDA, ValidarExistenciaRolPorIdDA>();
builder.Services.AddTransient<IValidarExistenciaRolPorIdLN, ValidarExistenciaRolPorIdLN>();
builder.Services.AddTransient<IObtenerRolesPorIdUsuarioDA, ObtenerRolesPorIdUsuarioDA>();
builder.Services.AddTransient<IObtenerRolesPorIdUsuarioLN, ObtenerRolesPorIdUsuarioLN>();

//Estados
builder.Services.AddTransient<IObtenerEstadoPorIdDA, ObtenerEstadoPorIdDA>();
builder.Services.AddTransient<IObtenerEstadoPorIdLN, ObtenerEstadoPorIdLN>();
builder.Services.AddTransient<IListarEstadosDA, ListarEstadosDA>();
builder.Services.AddTransient<IListarEstadosLN, ListarEstadosLN>();

//Usuarios
builder.Services.AddTransient<IListarUsuariosLN, ListarUsuariosLN>();
builder.Services.AddTransient<IListarUsuariosDA, ListarUsuariosDA>();
builder.Services.AddTransient<IObtenerUsuarioPorIdentificacionLN, ObtenerUsuarioPorIdentificacionLN>();
builder.Services.AddTransient<IObtenerUsuarioPorIdentificacionDA, ObtenerUsuarioPorIdentificacionDA>();
builder.Services.AddTransient<IObtenerUsuarioPorIdLN, ObtenerUsuarioPorIdLN>();
builder.Services.AddTransient<IObtenerUsuarioPorIdDA, ObtenerUsuarioPorIdDA>();
builder.Services.AddTransient<IEliminarUsuarioDA, EliminarUsuarioDA>();
builder.Services.AddTransient<IEliminarUsuarioLN, EliminarUsuarioLN>();
builder.Services.AddTransient<ICrearUsuarioDA, CrearUsuarioDA>();
builder.Services.AddTransient<ICrearUsuarioLN, CrearUsuarioLN>();
builder.Services.AddTransient<IAsignarRolesDA, AsignarRolesDA>();
builder.Services.AddTransient<IAsignarRolesLN, AsignarRolesLN>();
builder.Services.AddTransient<IModificarUsuarioDA, ModificarUsuarioDA>();
builder.Services.AddTransient<IModificarUsuarioLN, ModificarUsuarioLN>();
builder.Services.AddTransient<IRegistrarUsuarioLN, RegistrarUsuarioLN>();
builder.Services.AddTransient<IObtenerUsuarioDtoPorIdDA, ObtenerUsuarioDtoPorIdDA>();
builder.Services.AddTransient<IObtenerUsuarioDtoPorIdLN, ObtenerUsuarioDtoPorIdLN>();
builder.Services.AddTransient<ILoginDA, LoginDA>();
builder.Services.AddTransient<ILoginLN, LoginLN>();

//Cliente
builder.Services.AddTransient<IActualizarClienteAsyncAD, ActualizarClienteAsyncAD>();
builder.Services.AddTransient<IActualizarClienteAsyncLN, ActualizarClienteAsyncLN>();
builder.Services.AddTransient<ICrearClienteAsyncAD, CrearClienteAsyncAD>();
builder.Services.AddTransient<ICrearClienteAsyncLN, CrearClienteAsyncLN>();
builder.Services.AddTransient<IEliminarClienteAsyncAD, EliminarClienteAsyncAD>();
builder.Services.AddTransient<IEliminarClienteAsyncLN, EliminarClienteAsyncLN>();
builder.Services.AddTransient<IObtenerClienteAsyncAD, ObtenerClienteAsyncAD>();
builder.Services.AddTransient<IObtenerClienteAsyncLN, ObtenerClienteAsyncLN>();

builder.Services.AddTransient<IObtenerClientePorIdAsyncAD, ObtenerClientePorIdAsyncAD>();
builder.Services.AddTransient<IObtenerClientePorIdAsyncLN, ObtenerClientePorIdAsyncLN>();

// ***** NUEVO MÓDULO: SOLICITUDES DE CRÉDITO *****
builder.Services.AddTransient<ISolicitudCreditoDA, SolicitudCreditoAD>();
builder.Services.AddTransient<ISolicitudCreditoLN, SolicitudCreditoLN>();



//BITACORA
builder.Services.AddTransient<ICrearBitacoraAD, CrearBitacoraAD>();
builder.Services.AddTransient<ICrearBitacoraLN, CrearBitacoraLN>();
builder.Services.AddTransient<IListarBitacoraLN, ListarBitacoraLN>();
builder.Services.AddTransient<IListarBitacoraAD, ListarBitacoraAD>();

#endregion

builder.Services.AddTransient<IEmailSender,SmtpEmailSender>();

// Add services to the container. /**DB CONTEXT**/
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

// DB CONTEXT
var connectionString = builder.Configuration.GetConnectionString("ContextoConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<UsuarioDA, RolDA>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<Contexto>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Usuario/Login";  
    options.AccessDeniedPath = "/Home/AccesoDenegado";
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // <<< NECESARIO
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.MapRazorPages();
app.Run();
