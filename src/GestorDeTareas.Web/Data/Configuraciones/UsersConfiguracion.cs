using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GestorDeTareas.Web.Data.Configuraciones;

/// <summary>
/// /Configura por defuatl usuarios en la base de datos 
/// </summary>
public class UsersConfiguracion : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        var usuarioDefoult = new Usuario
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "admin",
            NormalizedUserName = "admin".ToUpper(),
            Email = "admin@shop.com",
            NormalizedEmail = "admin@shop.com".ToUpper(),
            EmailConfirmed = true
        };

        var passworHasher = new PasswordHasher<Usuario>();
        usuarioDefoult.PasswordHash = passworHasher.HashPassword(usuarioDefoult, "admin@22");
        builder.HasData(usuarioDefoult);

    }
}
