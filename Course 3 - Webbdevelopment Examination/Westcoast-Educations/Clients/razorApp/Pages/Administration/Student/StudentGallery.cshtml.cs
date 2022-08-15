using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels.Student;

namespace razorApp.Pages.Administration.Student
{
  public class StudentGallery : PageModel
  {
    private readonly ILogger<StudentGallery> _logger;
    private readonly HttpClient _http;
    private readonly string _baseUrl;
    private readonly IConfiguration _config;

    [BindProperty]
    public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();

    public StudentGallery(ILogger<StudentGallery> logger, IConfiguration config)
    {
      _logger = logger;
      _config = config;
      _http = new HttpClient();
      _baseUrl = _config.GetValue<string>("baseUrl");
    }

    public async Task<IActionResult> OnGet()
    {
      var url = $"{_baseUrl}/students";
      var students = await _http.GetFromJsonAsync<List<StudentViewModel>>(url);
      Students = students;
      _http.Dispose();
      return Page();

    }
  }
}
