using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels;
using razorApp.ViewModels.Teacher;

namespace razorApp.Pages.Administration.Teacher
{
  public class CreateTeacher : PageModel
  {
    private readonly ILogger<CreateTeacher> _logger;
    [BindProperty]
    public CreateTeacherViewModel TeacherModel { get; set; }
    private readonly IConfiguration _config;
    private readonly string BaseUrl;
    private readonly HttpClient _http;


    private List<string> AllSkills { get; set; } = new List<string>();
    [BindProperty]
    public List<EditTeacherSkillViewModel> SkillCheckboxList { get; set; } = new List<EditTeacherSkillViewModel>();

    public CreateTeacher(ILogger<CreateTeacher> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _http = new HttpClient();
    }

    public async Task OnGet()
    {

      AllSkills = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/skills/list/names");
      _http.Dispose();

      for (int i = 0; i < AllSkills.Count(); i++)
      {
        SkillCheckboxList.Add(new EditTeacherSkillViewModel
        {
          Name = AllSkills[i],
          IsChecked = false,
        });
      }
    }


    public async Task OnPostAsync()
    {
      foreach (var skill in SkillCheckboxList)
      {
        if (skill.IsChecked == true)
        {
          TeacherModel.Skills.Add(skill.Name);
        }
      }

      var baseUrl = _config.GetValue<string>("baseUrl");
      var url = $"{baseUrl}/teachers";
      var response = await _http.PostAsJsonAsync(url, TeacherModel);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
      }
      else
      {
        Console.WriteLine("Läraren lades till");
      }
      _http.Dispose();




    }
  }
}
