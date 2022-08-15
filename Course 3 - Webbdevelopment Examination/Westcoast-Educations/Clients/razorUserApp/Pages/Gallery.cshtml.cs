using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace razorApp.Pages
{
  public class Gallery : PageModel
  {
    private readonly ILogger<Gallery> _logger;
    private readonly HttpClient _http;
    private readonly string _baseUrl;
    [BindProperty(SupportsGet = true)]
    public string Category { get; set; }
    private readonly IConfiguration _config;
    [BindProperty]
    public List<string> AllCategories { get; set; } = new List<string>();

    [BindProperty]
    public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

    public Gallery(ILogger<Gallery> logger, IConfiguration config)
    {
      _logger = logger;
      _config = config;
      _http = new HttpClient();
      _baseUrl = _config.GetValue<string>("baseUrl");
    }

    public async Task<IActionResult> OnGet()
    {
      var url = $"{_baseUrl}/courses";

      if(!string.IsNullOrEmpty(Category))
      {
          url += $"/byCategory/{System.Web.HttpUtility.UrlEncode(Category)}";
      }
      var courses = await _http.GetFromJsonAsync<List<CourseViewModel>>(url);

      Courses = courses;
      using (_http)
      {
        AllCategories = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/categories/list/names");

      }

      return Page();

    }

    

  }
}
