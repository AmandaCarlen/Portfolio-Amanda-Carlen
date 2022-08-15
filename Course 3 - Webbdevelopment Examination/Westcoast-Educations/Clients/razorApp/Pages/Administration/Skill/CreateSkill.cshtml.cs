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
  public class CreateSkill : PageModel
  {
    private readonly ILogger<CreateSkill> _logger;
    [BindProperty]
    public CreateSkillViewModel SkillModel { get; set; }

    private readonly IConfiguration _config;
    private readonly HttpClient _http;

    public CreateSkill(ILogger<CreateSkill> logger, IConfiguration config)
    {
      _logger = logger;
      _config = config;
      _http = new HttpClient();

    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
      var baseUrl = _config.GetValue<string>("baseUrl");
      var url = $"{baseUrl}/skills";
      var response = await _http.PostAsJsonAsync(url, SkillModel);
      _http.Dispose();

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
      }
    }
  }
}
