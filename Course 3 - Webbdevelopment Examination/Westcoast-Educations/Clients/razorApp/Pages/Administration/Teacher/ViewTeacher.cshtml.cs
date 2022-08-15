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
  public class ViewTeacher : PageModel
  {
    private readonly ILogger<ViewTeacher> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    [BindProperty]
    public TeacherViewModel TeacherModel { get; set; }

    public ViewTeacher(ILogger<ViewTeacher> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _baseUrl = _config.GetValue<string>("baseUrl");
      _http = new HttpClient();
    }

    public async Task<IActionResult> OnGet(int id)
    {
      var url = $"{_baseUrl}/teachers/{id}";
      var teacher = await _http.GetFromJsonAsync<TeacherViewModel>(url);
      _http.Dispose();

      var teacherToView = new TeacherViewModel
      {
        Id = teacher.Id,
        FirstName = teacher.FirstName,
        LastName = teacher.LastName,
        Email = teacher.Email,
        PhoneNumber = teacher.PhoneNumber,
        StreetAdress = teacher.StreetAdress,
        ZipCode = teacher.ZipCode,
        City = teacher.City,
        Country = teacher.Country,
        Skill = teacher.Skill,
        Course = teacher.Course
      };

      TeacherModel = teacherToView;
      return Page();

    }
  }
}
