using GestorDeTareas.Web.Data;
using GestorDeTareas.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//ar connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AgregarDbContext(builder.Configuration, useInMemory: true);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<TareasContext>();

builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToFolder("/Cuenta");
    options.Conventions.AllowAnonymousToPage("/Cuenta/Registro");
    options.Conventions.AuthorizePage("/Index");
});

builder.Services.ConfigureApplicationCookie(cookieOptions =>
{
    cookieOptions.LoginPath = "/Cuenta/Login";
    cookieOptions.LogoutPath = "/";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    // Ejecuta una migracion a la Base de datos.
    //Usar solo en Development !!
  //ServicioMigracionData.InicializarMigracionDeDatos(app);
}
else
{
   // ServicioMigracionData.InicializarMigracionDeDatos(app);
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
