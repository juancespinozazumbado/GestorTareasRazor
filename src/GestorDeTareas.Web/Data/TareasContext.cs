using GestorDeTareas.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GestorDeTareas.Web.Data;

public class TareasContext : IdentityDbContext<Usuario>
{
    public TareasContext(DbContextOptions<TareasContext> options)
        : base(options)
    {
    }

    // DbSet para manejar las entidades Tarea
    public DbSet<Tarea> Tareas { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Se aplican todas las configuraciones del tipo IEntityTypeConfiguration

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   
    }

}

public class Usuario : IdentityUser
{
    public string Nombre { get; set; } = string.Empty;
}
