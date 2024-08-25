using GestorDeTareas.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.Web.Pages.Cuenta
{
    public class RegistroModel : PageModel
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<RegistroModel> _logger;

        public RegistroModel(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ILogger<RegistroModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
           // [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var data = Input;

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "The password and confirmation password do not match.");

                return Page();
            }

            var user = new Usuario { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                return RedirectToPage("/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }


        public void OnGet()
        {

        }
    }
}
