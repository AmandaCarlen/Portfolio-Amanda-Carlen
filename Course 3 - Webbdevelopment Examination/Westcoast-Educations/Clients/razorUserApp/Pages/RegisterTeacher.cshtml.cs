using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels;
using razorApp.ViewModels.Teacher;

namespace razorUserApp.Pages
{
    public class RegisterTeacher : PageModel
    {
        private readonly ILogger<RegisterTeacher> _logger;
           [BindProperty]
    public CreateTeacherViewModel TeacherModel { get; set; }
    private readonly IConfiguration _config;
    private readonly string BaseUrl;
    private readonly HttpClient _http;


    private List<string> AllSkills {get; set;} = new List<string>();
    [BindProperty]
    public List<EditTeacherSkillViewModel> SkillCheckboxList {get; set;} = new List<EditTeacherSkillViewModel>();

        public RegisterTeacher(ILogger<RegisterTeacher> logger, IConfiguration config)
        {
            _config = config;
      _logger = logger;
      _http = new HttpClient();
        }

        public async Task OnGet()
    {
        using(_http)
        {
            AllSkills = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/skills/list/names");
        }
        for(int i = 0; i< AllSkills.Count(); i++)
        {
            SkillCheckboxList.Add(new EditTeacherSkillViewModel{
                Name = AllSkills[i],
                IsChecked = false,
            });
        }
    }

    // Kan lägga till Task<Iactionresult> och skicka tillbaka till en sida
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

      using (_http)
      {
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
      }


    }
    }
}
