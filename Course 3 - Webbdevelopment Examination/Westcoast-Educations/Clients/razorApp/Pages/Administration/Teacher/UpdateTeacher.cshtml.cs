using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels;
using razorApp.ViewModels.Course;
using razorApp.ViewModels.Teacher;

namespace razorApp.Pages.Administration.Teacher
{
  public class UpdateTeacher : PageModel
  {
    private readonly ILogger<UpdateTeacher> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    [BindProperty]
    public UpdateTeacherViewModel TeacherModel { get; set; }
    private List<string> AllCourses { get; set; } = new List<string>();
    private List<string> TeacherCourses { get; set; } = new List<string>();
    private List<string> AllSkills { get; set; } = new List<string>();
    private List<string> TeacherSkills { get; set; } = new List<string>();


    [BindProperty]

    public List<EditUserCourseViewModel> CourseCheckboxList { get; set; } = new List<EditUserCourseViewModel>();
    [BindProperty]
    public List<EditTeacherSkillViewModel> SkillCheckboxList { get; set; } = new List<EditTeacherSkillViewModel>();


    private readonly IConfiguration _config;

    public UpdateTeacher(ILogger<UpdateTeacher> logger, IConfiguration config)
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
      var teacherToUpdate = new UpdateTeacherViewModel
      {
        Id = teacher.Id,
        FirstName = teacher.FirstName,
        LastName = teacher.LastName,
        PhoneNumber = teacher.PhoneNumber,
        StreetAdress = teacher.StreetAdress,
        ZipCode = teacher.ZipCode,
        City = teacher.City,
        Country = teacher.Country
      };

      TeacherModel = teacherToUpdate;
     
        AllSkills = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/skills/list/names");
        AllCourses = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/courses/list/names");
        TeacherCourses = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/courses/list/teachercourse/{teacher.Id}");
        TeacherSkills = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/teachers/list/names/connected/{teacher.Id}");
      
      _http.Dispose();

      for (int i = 0; i < AllSkills.Count(); i++)
      {
        var nameInList = new EditTeacherSkillViewModel();
        nameInList.Name = AllSkills[i];
        if (TeacherSkills == null)
        {
          nameInList.IsChecked = false;
        }
        else
        {
          foreach (var skill in TeacherSkills)
          {
            if (skill.ToLower() == AllSkills[i].ToLower())
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
        SkillCheckboxList.Add(nameInList);
      }
      for (int i = 0; i < AllCourses.Count(); i++)
      {
        var nameInList = new EditUserCourseViewModel();
        nameInList.Name = AllCourses[i];
        if (TeacherCourses == null)
        {
          nameInList.IsChecked = false;
        }
        else
        {
          foreach (var course in TeacherCourses)
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
       if (!ModelState.IsValid){
            
        }
        foreach(var skill in SkillCheckboxList)
        {
            if(skill.IsChecked == true)
            {
            TeacherModel.Skills.Add(skill.Name);
            }
        }
        foreach(var course in CourseCheckboxList)
        {
            if(course.IsChecked == true)
            {
            TeacherModel.Courses.Add(course.Name);
            }
        }
     
      var url = $"{_baseUrl}/teachers/{TeacherModel.Id}";
      var response = await _http.PutAsJsonAsync(url, TeacherModel);
      _http.Dispose();

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response);
      }
      
    }
  }
}
