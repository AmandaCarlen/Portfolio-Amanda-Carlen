using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels.Categories;

namespace razorApp.Pages.Administration.Category
{
  public class UpdateCategory : PageModel
  {
    private readonly ILogger<UpdateCategory> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    [BindProperty]
    public UpdateCategoryViewModel CategoryModel { get; set; }

    public UpdateCategory(ILogger<UpdateCategory> logger, IConfiguration config)
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
      var categoryToUpdate = new UpdateCategoryViewModel
      {
        Id = category.Id,
        Name = category.Name,
      };

      CategoryModel = categoryToUpdate;
       _http.Dispose();
      return Page();
    }
    public async Task OnPostAsync()
    {
      var url = $"{_baseUrl}/Categories/{CategoryModel.Id}";
      var response = await _http.PutAsJsonAsync(url, CategoryModel);
       _http.Dispose();

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response);
      }
    }
  }
}
