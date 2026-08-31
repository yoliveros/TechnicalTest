using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FrontRazor.Pages
{
    public class LoginModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = httpClientFactory.CreateClient("WebApi");

            try
            {
                var response = client.PostAsJsonAsync("api/auth/login", Input).Result;
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return Page();
                }
                var tokenResponse = response.Content.ReadFromJsonAsync<TokenResponse>().Result;

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, Input.Username),
                    new("JWToken", tokenResponse?.Token ?? string.Empty)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity)).Wait();
                //HttpContext.Session.SetString("JWToken", tokenResponse?.Token ?? string.Empty);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while logging in: {ex.Message}");
                return Page();

            }

            return RedirectToPage("/Index");
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; } = string.Empty;
            [Required]
            public string Password { get; set; } = string.Empty;
        }

        public class TokenResponse
        {
            public string Token { get; set; } = string.Empty;
        }
    }
}
