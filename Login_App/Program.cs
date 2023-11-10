using Microsoft.EntityFrameworkCore;
using Login_App.Models;

using Login_App.Servicios.Contrato;
using Login_App.Servicios.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// -----
// Configuramos el Contexto de la Base de Datos para poder usarlo en el proyecto. 
builder.Services.AddDbContext<BdloginContext>(options =>
{
    // Toma los datos del 'appsettings.json'.
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

// Así podremos usar este servicio dentro de cualquier controlador que creemos.
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
// -----

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
