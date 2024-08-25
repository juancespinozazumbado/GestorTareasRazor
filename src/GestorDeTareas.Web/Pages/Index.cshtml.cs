using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeTareas.Web.Pages
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<FileModel> Files { get; set; }

        public void OnGet()
        {
            // Simulate fetching files from a database or storage
            Files = new List<FileModel>
        {
            new FileModel { Name = "Document1.docx", Size = 150 },
            new FileModel { Name = "Image1.jpg", Size = 2048 },
            new FileModel { Name = "Presentation.pptx", Size = 532 }
        };
        }
    }

    public class FileModel
    {
        public string Name { get; set; }
        public int Size { get; set; } // In KB
    }
}

