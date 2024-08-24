using GestorDeTareas.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Web.Services;

public static class ServicioMigracionData
{
    public static async void InicializarMigracionDeDatos(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var dbCOntext = serviceScope.ServiceProvider.GetRequiredService<TareasContext>();

            //Usar esta configuracion para SQLite 

            //Usar esta configuracion para SQL Server
            await dbCOntext.Database.MigrateAsync();

            var result = dbCOntext.Database.EnsureCreated();


        }
    }
}
