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
  public class ViewStudent : PageModel
  {
    private readonly ILogger<ViewStudent> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    [BindProperty]
    public StudentViewModel StudentModel { get; set; }
    private readonly IConfiguration _config;

    public ViewStudent(ILogger<ViewStudent> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _baseUrl = _config.GetValue<string>("baseUrl");
      _http = new HttpClient();
    }


    public async Task<IActionResult> OnGet(int id)
    {
      var url = $"{_baseUrl}/students/{id}";
      var student = await _http.GetFromJsonAsync<StudentViewModel>(url);
      _http.Dispose();

      var studentToView = new StudentViewModel
      {
        Id = student.Id,
        FirstName = student.FirstName,
        LastName = student.LastName,
        PhoneNumber = student.PhoneNumber,
        StreetAdress = student.StreetAdress,
        ZipCode = student.ZipCode,
        City = student.City,
        Country = student.Country,
        Course = student.Course
      };

      StudentModel = studentToView;
      return Page();

    }
  }
}
