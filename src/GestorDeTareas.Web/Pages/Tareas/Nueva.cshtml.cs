using GestorDeTareas.Web.Data;
using GestorDeTareas.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Web.Pages.Tareas
{
    [Authorize]
    public class NuevaModel : PageModel
    {
        private readonly TareasContext _context;
        private readonly ILogger<NuevaModel> _logger; 
        
        public NuevaModel(TareasContext tareasContext, ILogger<NuevaModel> logger)
        {
            _context = tareasContext;
            _logger = logger;
        }


        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            public string Titulo { get; set; }= string.Empty;

            [Required]
            [DataType("Date")]
            
            public DateOnly FechaFinalizacion { get; set; }
        }


        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _context.Tareas.AddAsync(new Tarea
                {
                    UsuarioId = User.Identity.Name,
                    Titulo = Input.Titulo,
                    //FechaFinalizacion = Input.FechaFinalizacion,
                    IsTerminada = false
                });

                await _context.SaveChangesAsync();
                return RedirectToPage("/");



            }catch(Exception ex)
            {
                _logger.LogError("Error {ex}", ex.Message);
                return Page();  
            }
        }
        public void OnGet()
        {
        }
    }
}
