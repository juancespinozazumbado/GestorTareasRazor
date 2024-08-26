using GestorDeTareas.Web.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Web.Pages.Cuenta
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager; 
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var usuario = await _userManager.FindByEmailAsync(Input.Email);
            if (usuario == null)
            {
                ModelState.AddModelError("Error", "Usuario no existe!");
                return Page();

            }

            var resultado = await _signInManager.PasswordSignInAsync(user: usuario, 
                password: Input.Password, isPersistent: true, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToPage("/Index");
            }else
            {
                ModelState.AddModelError("error", "Intento de inicio de sescion fallido");
                return Page();
            }
            
        }

        //Cuenta/Login?handler=Logout
        public async Task<IActionResult> OnPostLogout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return new JsonResult(new { status = "Ok" });


            }catch(Exception ex)
            {

                _logger.LogError("excpecion {ex}", ex.Message);
                return BadRequest();

            }

        }

        public void OnGet() { }
    }
}
