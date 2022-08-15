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
  public class CreateCategory : PageModel
  {
    private readonly ILogger<CreateCategory> _logger;
    [BindProperty]
    public CreateCategoryViewModel CategoryModel { get; set; }

    private readonly IConfiguration _config;
    private readonly HttpClient _http;

    public CreateCategory(ILogger<CreateCategory> logger, IConfiguration config)
    {
      _logger = logger;
      _config = config;
      _http = new HttpClient();
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
      var baseUrl = _config.GetValue<string>("baseUrl");
      var url = $"{baseUrl}/categories";
      var response = await _http.PostAsJsonAsync(url, CategoryModel);
      _http.Dispose();


      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
      }
    }
  }
}
