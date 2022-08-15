using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels;
using razorApp.ViewModels.Skills;
using razorApp.ViewModels.Teacher;

namespace razorApp.Pages.Administration
{
  public class CreateCourse : PageModel
  {
    private readonly ILogger<CreateCourse> _logger;

    [BindProperty]
    public CreateCourseViewModel CourseModel { get; set; }

    private readonly IConfiguration _config;
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public CreateCourse(ILogger<CreateCourse> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _http = new HttpClient();
      _baseUrl = _config.GetValue<string>("baseUrl");

    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {  
      var url = $"{_baseUrl}/courses";
      var response = await _http.PostAsJsonAsync(url, CourseModel);
      _http.Dispose();

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
      }
    }
  }
}
