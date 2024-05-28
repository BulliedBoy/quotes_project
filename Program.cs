using Microsoft.EntityFrameworkCore;
using quotes_project.Views.Home.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios al contenedor
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Agregar servicio de sesi�n

// Configurar el contexto de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Crear el app
var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession(); // Usar sesi�n

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
