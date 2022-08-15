using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels;

namespace razorApp.Pages.Administration.Course
{
  public class CourseGallery : PageModel
  {
    private readonly ILogger<CourseGallery> _logger;
    private readonly HttpClient _http;
    private readonly string _baseUrl;
    private readonly IConfiguration _config;
    [BindProperty]
    public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

    public CourseGallery(ILogger<CourseGallery> logger, IConfiguration config)
    {
      _logger = logger;
      _config = config;
      _http = new HttpClient();
      _baseUrl = _config.GetValue<string>("baseUrl");
    }

    public async Task<IActionResult> OnGet()
    {
      var url = $"{_baseUrl}/courses";
      var courses = await _http.GetFromJsonAsync<List<CourseViewModel>>(url);
      Courses = courses;
      _http.Dispose();
      return Page();
    }
  }
}
