using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels.Teacher;

namespace razorApp.Pages.Administration.Teacher
{
  public class DeleteTeacher : PageModel
  {
    private readonly ILogger<DeleteTeacher> _logger;

    private readonly string _baseUrl;
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    [BindProperty]
    public TeacherViewModel Teacher { get; set; }

    public DeleteTeacher(ILogger<DeleteTeacher> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _baseUrl = _config.GetValue<string>("baseUrl");
      _http = new HttpClient();
    }

    public async Task<IActionResult> OnGet(int id)
    {
      var url = $"{_baseUrl}/teachers/{id}";
      Teacher = await _http.GetFromJsonAsync<TeacherViewModel>(url);
      _http.Dispose();
      return Page();

    }

    public async Task<IActionResult> OnPost()
    {
      var url = $"{_baseUrl}/teachers/{Teacher.Id}";
      var response = await _http.DeleteAsync(url);
      _http.Dispose();

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response);
      }

      return RedirectToPage("./TeacherGallery");


    }
  }
}
