using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace FrontRazor.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public ProductModel Product { get; set; } = new ProductModel();

        public List<ProductModel> Products { get; set; } = new List<ProductModel>();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("WebApi");
            try
            {
                var response = await client.GetAsync("api/products");

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve products.");
                    return;
                }

                Products = await response.Content.ReadFromJsonAsync<List<ProductModel>>() ?? new List<ProductModel>();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving products: {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                ModelState.AddModelError(string.Empty, "Invalid product");
                return StatusCode(400);
            }

            var client = _httpClientFactory.CreateClient("WebApi");
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                var response = await client.PostAsJsonAsync("api/products", Product);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create product.");
                    await OnGetAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating product: {ex.Message}");
                await OnGetAsync();
                return Page();
            }

            return RedirectToPage();
        }

        public class ProductModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalValue => Quantity * UnitPrice;
        }
    }
}
