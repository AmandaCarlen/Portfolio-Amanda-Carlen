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
  public class UpdateSkill : PageModel
  {
    private readonly ILogger<UpdateSkill> _logger;
    private readonly string _baseUrl;
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    [BindProperty]
    public UpdateSkillViewModel SkillModel { get; set; }

    public UpdateSkill(ILogger<UpdateSkill> logger, IConfiguration config)
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
      var skillToUpdate = new UpdateSkillViewModel
      {
        Id = skill.Id,
        Name = skill.Name,
      };

      SkillModel = skillToUpdate;
      return Page();

    }
    public async Task OnPostAsync()
    {
      var url = $"{_baseUrl}/Skills/{SkillModel.Id}";
      var response = await _http.PutAsJsonAsync(url, SkillModel);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response);
      }
    }
  }
}
