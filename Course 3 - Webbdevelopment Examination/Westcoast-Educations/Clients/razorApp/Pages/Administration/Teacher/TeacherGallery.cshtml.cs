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
  public class TeacherGallery : PageModel
  {
    private readonly ILogger<TeacherGallery> _logger;
    private readonly HttpClient _http;
    private readonly string _baseUrl;
    private readonly IConfiguration _config;
    [BindProperty(SupportsGet = true)]
    public string Skill { get; set; }
    [BindProperty]
    public List<string> AllSkills { get; set; } = new List<string>();

    [BindProperty]
    public List<TeacherViewModel> Teachers { get; set; } = new List<TeacherViewModel>();

    public TeacherGallery(ILogger<TeacherGallery> logger, IConfiguration config)
    {
      _logger = logger;
      _config = config;
      _http = new HttpClient();
      _baseUrl = _config.GetValue<string>("baseUrl");
    }

    public async Task<IActionResult> OnGet()
    {
      var url = $"{_baseUrl}/teachers";

      if (!string.IsNullOrEmpty(Skill))
      {
        url += $"/byskill/{System.Web.HttpUtility.UrlEncode(Skill)}";
      }
      var teachers = await _http.GetFromJsonAsync<List<TeacherViewModel>>(url);

      Teachers = teachers;
      AllSkills = await _http.GetFromJsonAsync<List<string>>($"{_config.GetValue<string>("baseUrl")}/skills/list/names");


      _http.Dispose();


      return Page();


    }
  }
}
