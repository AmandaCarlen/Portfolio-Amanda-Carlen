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
  public class DeleteCourse : PageModel
  {
    private readonly ILogger<DeleteCourse> _logger;
    private readonly string _baseUrl;

    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    [BindProperty]
    public CourseViewModel Course { get; set; }

    public DeleteCourse(ILogger<DeleteCourse> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _baseUrl = _config.GetValue<string>("baseUrl");
      _http = new HttpClient();
    }

    public async Task<IActionResult> OnGet(int id)
    {
      var url = $"{_baseUrl}/courses/{id}";
      Course = await _http.GetFromJsonAsync<CourseViewModel>(url);
      _http.Dispose();
      return Page();

    }

    public async Task<IActionResult> OnPost()
    {
      var url = $"{_baseUrl}/courses/{Course.Id}";
      var response = await _http.DeleteAsync(url);
      _http.Dispose();

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response);
      }

      return RedirectToPage("./CourseGallery");


    }
  }
}
