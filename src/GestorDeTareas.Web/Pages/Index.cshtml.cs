using GestorDeTareas.Web.Data;
using GestorDeTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace GestorDeTareas.Web.Pages
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TareasContext _context;

        [BindProperty]
        public Tarea Tarea { get; set; }
        public IndexModel(ILogger<IndexModel> logger, TareasContext contex)
        {
            _logger = logger;
            _context = contex;
          
        }

        [BindProperty]
        public List<Tarea> Tareas { get; set; }

        public IActionResult OnGetTareas(string searchQuery = null)
        {
            var tareas = _context.Tareas.AsQueryable();

            // primero aseguramos que pertenecen al usuario

            tareas = tareas.Where(tarea => tarea.UsuarioId.Equals(User.Identity.Name));

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var query = searchQuery.ToLower();
                //tareas = tareas.Where(tarea => tarea.Titulo.Contains(searchQuery) || tarea.Descripcion.Contains(searchQuery));
                 tareas = tareas.Where(tarea => tarea.Titulo.ToLower().Contains(query) || tarea.Descripcion.ToLower().Contains(searchQuery));
              
            }
            Tareas = tareas.ToList();   

            return new JsonResult(Tareas);

        }

        public IActionResult OnGetDetallesTarea(string id)
        {
           var tarea = _context.Tareas.FirstOrDefault(tarea => tarea.Id == new Guid(id));
           if(tarea == null)
            {
                return NotFound();
            }

            return new JsonResult(new
            {
                id = tarea.Id,
                titulo = tarea.Titulo,
                descripcion = tarea.Descripcion,
                fechaFinalizacion = tarea.FechaFinalizacion.ToString("yyyy-MM-dd"), 
                isTerminada = tarea.IsTerminada
            });
        }

       public IActionResult OnGetFiltarTarea(string parametro = null)
        {
            
            var tareas = _context.Tareas.Where( tarea => tarea.Titulo.Contains(parametro) || tarea.Descripcion.Contains(parametro));

            if (tareas.Any())
            {
                return new JsonResult(tareas);
            }
            else return new JsonResult(new { });
        }


        public IActionResult OnPostEliminar(string id)
        {
            var tarea = _context.Tareas.Where(tarea => tarea.Id == new Guid(id)).FirstOrDefault();
            if(tarea != null)
            {
                _context.Tareas.Remove(tarea); 
                _context.SaveChanges();
              
                return new JsonResult(new { success = true});
            }else
            {
                return new JsonResult(new {success = false});
            }
        }

        public IActionResult OnPostMarcarTarea(string id)
        {
            var tarea = _context.Tareas.FirstOrDefault(tarea => tarea.Id == new Guid(id));
            if (tarea != null)
            {
               tarea.IsTerminada = !tarea.IsTerminada;
                _context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            else
            {
                return new JsonResult(new { success = false });
            }
        }


        public async Task<IActionResult> OnPostCrearTarea(Tarea tarea)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var nuevaTarea = new Tarea { 
                Id = Guid.NewGuid(), Titulo = tarea.Titulo, 
                FechaFinalizacion = tarea.FechaFinalizacion, 
                Descripcion = tarea.Descripcion, IsTerminada = false,
                UsuarioId = User.Identity.Name};
           _context.Tareas.Add(nuevaTarea);  
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });

        }

        public async Task<IActionResult> OnPostEditarTarea(Tarea tarea)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();

            }

            var treaporEditar = _context.Tareas.FirstOrDefault(t => t.Id == tarea.Id);
            if(tarea == null)
            {
                return BadRequest();
            }

            treaporEditar.Titulo = tarea.Titulo;
            treaporEditar.Descripcion = tarea.Descripcion;
            treaporEditar.FechaFinalizacion = tarea.FechaFinalizacion;

            _context.Tareas.Update(treaporEditar);

            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });

        }



    }
}




