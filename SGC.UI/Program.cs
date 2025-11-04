using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Cliente;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.AccesoDatos;
using SGC.AccesoDatos.Cliente;
using SGC.AccesoDatos.Roles;
using SGC.LogicaDeNegocio;
using SGC.LogicaDeNegocio.Cliente;
using SGC.LogicaDeNegocio.Mapper;
using SGC.LogicaDeNegocio.Roles;

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


//Usuarios

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



#endregion

builder.Services.AddTransient<IEmailSender,SmtpEmailSender>();

// Add services to the container. /**DB CONTEXT**/

var connectionString = builder.Configuration.GetConnectionString("ContextoConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<UsuarioDA, RolDA>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<Contexto>()
.AddDefaultTokenProviders();

//-------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



app.Run();
