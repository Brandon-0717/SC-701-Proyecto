using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.AccesoDatos;
using SGC.AccesoDatos.Roles;
using SGC.LogicaDeNegocio;
using SGC.LogicaDeNegocio.Mapper;
using SGC.LogicaDeNegocio.Roles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

#region INYECCION DEPENDENCIAS
//Roles
builder.Services.AddTransient<IListarRolesAD, ListarRolesAD>();
builder.Services.AddTransient<IListarRolesLN, ListarRolesLN>();

//Usuarios


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
    options.SignIn.RequireConfirmedAccount = false;
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
