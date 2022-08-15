using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels;

namespace razorApp.Pages.Administration
{
  public class UpdateCourse : PageModel
  {
    private readonly ILogger<UpdateCourse> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;

    [BindProperty]
    public UpdateCourseViewModel CourseModel { get; set; }
    private readonly IConfiguration _config;

    public UpdateCourse(ILogger<UpdateCourse> logger, IConfiguration config)
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
      var courseToUpdate = new UpdateCourseViewModel
      {
        Id = course.Id,
        Title = course.Title,
        Length = course.Length,
        Category = course.Category,
        Description = course.Description,
        Details = course.Details
      };

      CourseModel = courseToUpdate;
      _http.Dispose();
      return Page();
    }

    public async Task OnPostAsync()
    {
      var url = $"{_baseUrl}/Courses/{CourseModel.Id}";
      var response = await _http.PutAsJsonAsync(url, CourseModel);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response);
      }
    }
  }
}
