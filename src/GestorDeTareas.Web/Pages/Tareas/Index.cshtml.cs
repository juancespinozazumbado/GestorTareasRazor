using GestorDeTareas.Web.Data;
using GestorDeTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeTareas.Web.Pages.Tareas
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TareasContext _contex;
        private readonly ILogger<IndexModel> _logger;   

        public IndexModel(TareasContext contex, ILogger<IndexModel> logger)
        {
            _contex = contex;
            _logger = logger;

        }

        [BindProperty]
        public IEnumerable<Tarea> Tareas { get; set; }

        public void OnGet()
        {
            Tareas = _contex.Tareas.Where( tarea => tarea.UsuarioId.Equals(User.Identity.Name)).AsEnumerable();

        }
    }
}
