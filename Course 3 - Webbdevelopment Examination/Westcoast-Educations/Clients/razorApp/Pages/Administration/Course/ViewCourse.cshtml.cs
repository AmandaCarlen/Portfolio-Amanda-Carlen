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
  public class ViewCourse : PageModel
  {
    private readonly ILogger<ViewCourse> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    [BindProperty]
    public CourseViewModel CourseModel { get; set; }
    private readonly IConfiguration _config;

    public ViewCourse(ILogger<ViewCourse> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _baseUrl = _config.GetValue<string>("baseUrl");
      _http = new HttpClient();
    }

    public async Task<IActionResult> OnGet(int id)
    {
      var url = $"{_baseUrl}/courses/{id}";
      var course = await _http.GetFromJsonAsync<CourseViewModel>(url);
      var courseToView = new CourseViewModel
      {
        Id = course.Id,
        Title = course.Title,
        Length = course.Length,
        Category = course.Category,
        Description = course.Description,
        Details = course.Details
      };
      _http.Dispose();
      CourseModel = courseToView;
      return Page();

    }
  }
}
