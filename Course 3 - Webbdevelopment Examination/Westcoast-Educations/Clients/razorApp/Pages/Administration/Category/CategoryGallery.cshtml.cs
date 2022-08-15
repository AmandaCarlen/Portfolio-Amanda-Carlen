using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels;

namespace razorApp.Pages.Administration.Category
{
  public class CategoryGallery : PageModel
  {
    private readonly ILogger<CategoryGallery> _logger;
    private readonly HttpClient _http;
    private readonly string _baseUrl;
    private readonly IConfiguration _config;
    [BindProperty]
    public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

    public CategoryGallery(ILogger<CategoryGallery> logger, IConfiguration config)
    {
      _logger = logger;
      _config = config;
      _http = new HttpClient();
      _baseUrl = _config.GetValue<string>("baseUrl");
    }

    public async Task<IActionResult> OnGet()
    {
      var url = $"{_baseUrl}/categories";
      var categories = await _http.GetFromJsonAsync<List<CategoryViewModel>>(url);
      Categories = categories;
      _http.Dispose();
      return Page();
    }
  }
}
