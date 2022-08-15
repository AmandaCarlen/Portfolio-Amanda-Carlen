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
  public class DeleteStudent : PageModel
  {
    private readonly ILogger<DeleteStudent> _logger;

    private readonly string _baseUrl;
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    [BindProperty]
    public StudentViewModel Student { get; set; }
    public DeleteStudent(ILogger<DeleteStudent> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _baseUrl = _config.GetValue<string>("baseUrl");
      _http = new HttpClient();
    }

    public async Task<IActionResult> OnGet(int id)
    {
      var url = $"{_baseUrl}/students/{id}";
      Student = await _http.GetFromJsonAsync<StudentViewModel>(url);
      return Page();
    }

    public async Task<IActionResult> OnPost()
    {
      var url = $"{_baseUrl}/students/{Student.Id}";
      var response = await _http.DeleteAsync(url);
      _http.Dispose();

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response);
      }

      return RedirectToPage("./StudentGallery");
    }
  }
}
