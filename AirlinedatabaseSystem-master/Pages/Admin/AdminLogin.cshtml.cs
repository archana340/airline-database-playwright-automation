using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirlinedatabaseSystem.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public AdminLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") == "true")
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string adminUsername = _configuration["AdminCredentials:Username"] ?? "";
            string adminPassword = _configuration["AdminCredentials:Password"] ?? "";

            if (Username == adminUsername && Password == adminPassword)
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                return RedirectToPage("/Index");
            }

            TempData["ErrorMessage"] = "Invalid username or password.";
            return Page();
        }
    }
}