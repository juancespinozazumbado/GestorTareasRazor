using GestorDeTareas.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeTareas.Web.Data.Configuraciones
{
    public class TareasConfiguracion : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
           // builder.Property<bool>("IsTerminada").HasColumnType("boolean");
        }
    }
}
