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
  public class ViewSkill : PageModel
  {
    private readonly ILogger<ViewSkill> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    [BindProperty]
    public SkillViewModel SkillModel { get; set; }
    private readonly IConfiguration _config;

    public ViewSkill(ILogger<ViewSkill> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _baseUrl = _config.GetValue<string>("baseUrl");
      _http = new HttpClient();
    }

    public async Task<IActionResult> OnGet(int id)
    {
      var url = $"{_baseUrl}/skills/{id}";
      var skill = await _http.GetFromJsonAsync<SkillViewModel>(url);
      var skillToView = new SkillViewModel
      {
        Id = skill.Id,
        Name = skill.Name,

      };

      SkillModel = skillToView;
      _http.Dispose();
      return Page();

    }
  }
}
