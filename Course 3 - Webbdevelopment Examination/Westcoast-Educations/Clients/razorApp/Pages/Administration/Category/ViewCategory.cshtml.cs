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
  public class ViewCategory : PageModel
  {
    private readonly ILogger<ViewCategory> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    [BindProperty]
    public CategoryViewModel CategoryModel { get; set; }
    private readonly IConfiguration _config;

    public ViewCategory(ILogger<ViewCategory> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _baseUrl = _config.GetValue<string>("baseUrl");
      _http = new HttpClient();
    }

    public async Task<IActionResult> OnGet(int id)
    {
      var url = $"{_baseUrl}/categories/{id}";
      var category = await _http.GetFromJsonAsync<CategoryViewModel>(url);
      var categoryToView = new CategoryViewModel
      {
        Id = category.Id,
        Name = category.Name,

      };

      CategoryModel = categoryToView;
      _http.Dispose();
      return Page();
    }
  }
}
