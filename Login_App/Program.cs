using Microsoft.EntityFrameworkCore;
using Login_App.Models;

using Login_App.Servicios.Contrato;
using Login_App.Servicios.Implementacion;

// Referencia para trabajar con las cookies.
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

// Forma para deshabilitar el caché.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore = true,
            Location = ResponseCacheLocation.None,
        }
     );
});

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

// -----
app.UseAuthentication();

// -----

app.UseAuthorization();

// Le indicamos el Controller y Acción con el que iniciaremos la app.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();
