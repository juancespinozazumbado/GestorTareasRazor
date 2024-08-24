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
            await dbCOntext.Database.MigrateAsync();


        }
    }
}
