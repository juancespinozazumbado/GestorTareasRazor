namespace GestorDeTareas.Web.Models;

public class Tarea
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion {  get; set; } = string.Empty;    
    public string UsuarioId { get; set; } = string.Empty;
    public DateTime FechaFinalizacion { get; set; } 
    public bool IsTerminada { get; set; } = false;
}
