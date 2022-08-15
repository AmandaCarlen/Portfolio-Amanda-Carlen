using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels.Course;
using razorApp.ViewModels.Student;

namespace razorApp.Pages.Administration.Student
{
  public class UpdateStudent : PageModel
  {
    private readonly ILogger<UpdateStudent> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    [BindProperty]
    public UpdateStudentViewModel StudentModel { get; set; }
    private List<string> AllCourses { get; set; } = new List<string>();

    private List<string> StudentCourses { get; set; } = new List<string>();
    [BindProperty]

    public List<EditUserCourseViewModel> CourseCheckboxList { get; set; } = new List<EditUserCourseViewModel>();

    private readonly IConfiguration _config;

    public UpdateStudent(ILogger<UpdateStudent> logger, IConfiguration config)
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
      var studentToUpdate = new UpdateStudentViewModel
      {
        Id = student.Id,
        FirstName = student.FirstName,
        LastName = student.LastName,
        PhoneNumber = student.PhoneNumber,
        StreetAdress = student.StreetAdress,
        ZipCode = student.ZipCode,
        City = student.City,
        Country = student.Country,
      };

      StudentModel = studentToUpdate;
    
        AllCourses = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/courses/list/names");
        StudentCourses = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/courses/list/StudentCourse/{student.Id}");
      
      _http.Dispose();
      
      for (int i = 0; i < AllCourses.Count(); i++)
      {
        var nameInList = new EditUserCourseViewModel();
        nameInList.Name = AllCourses[i];
        if (StudentCourses == null)
        {
          nameInList.IsChecked = false;
        }
        else
        {
          foreach (var course in StudentCourses)
          {
            if (course.ToLower() == AllCourses[i].ToLower())
            {
              nameInList.IsChecked = true;
              break;
            }
            else
            {
              nameInList.IsChecked = false;
            }
          }

        }
        CourseCheckboxList.Add(nameInList);
      }
      return Page();
    }
    public async Task OnPostAsync()
    {
       foreach(var course in CourseCheckboxList)
        {
            if(course.IsChecked == true)
            {
            StudentModel.Courses.Add(course.Name);
            }
        }

      var url = $"{_baseUrl}/Students/{StudentModel.Id}";
      var response = await _http.PutAsJsonAsync(url, StudentModel);
      _http.Dispose();


      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response);
      }
    }
  }
}
