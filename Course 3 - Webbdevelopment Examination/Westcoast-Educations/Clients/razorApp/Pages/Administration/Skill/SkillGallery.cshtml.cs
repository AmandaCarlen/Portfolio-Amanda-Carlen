using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels.Skills;

namespace razorApp.Pages.Administration.Skill
{
  public class SkillGallery : PageModel
  {
    private readonly ILogger<SkillGallery> _logger;
    private readonly HttpClient _http;
    private readonly string _baseUrl;
    private readonly IConfiguration _config;
    [BindProperty]
    public List<SkillViewModel> Skills { get; set; } = new List<SkillViewModel>();

    public SkillGallery(ILogger<SkillGallery> logger, IConfiguration config)
    {
      _logger = logger;
      _config = config;
      _http = new HttpClient();
      _baseUrl = _config.GetValue<string>("baseUrl");
    }

    public async Task<IActionResult> OnGet()
    {
      var url = $"{_baseUrl}/skills";
      var skills = await _http.GetFromJsonAsync<List<SkillViewModel>>(url);
      Skills = skills;
      _http.Dispose();
      return Page();
    }
  }
}
