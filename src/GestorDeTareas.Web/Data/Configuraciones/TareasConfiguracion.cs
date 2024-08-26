using GestorDeTareas.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading;

namespace GestorDeTareas.Web.Data.Configuraciones
{
    public class TareasConfiguracion : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
        //    // builder.Property<bool>("IsTerminada").HasColumnType("boolean");

        //    // Simulate fetching files from a database or storage
        //  var Tareas = new List<Tarea>
        //{
        //    new Tarea { Id=Guid.NewGuid(),  Titulo = "Tarea 1", FechaFinalizacion =  DateTime.Now, UsuarioId = "usuario-test", IsTerminada = false},
        //     new Tarea { Id=Guid.NewGuid(), Titulo = "Tarea 2", FechaFinalizacion =  DateTime.Now , UsuarioId= "usuario-test", IsTerminada = false},
        //       new Tarea {Id = Guid.NewGuid(),  Titulo = "Tarea 3", FechaFinalizacion =  DateTime.Now,  UsuarioId="usuario-test", IsTerminada = false},
        //         new Tarea { Id=Guid.NewGuid(),Titulo = "Tarea 4", FechaFinalizacion =  DateTime.Now,  UsuarioId= "usuario-test", IsTerminada = true},
        //           new Tarea { Id=Guid.NewGuid(),Titulo = "Tarea 5", FechaFinalizacion =  DateTime.Now,  UsuarioId= "usuario-test", IsTerminada = false},
        //};

        //    builder.HasData(Tareas);


        }
    }
}
