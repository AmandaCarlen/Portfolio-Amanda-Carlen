using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels.Student;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace razorApp.Pages.Administration.Student
{
  public class CreateStudent : PageModel
  {
    private readonly ILogger<CreateStudent> _logger;

    [BindProperty]
    public CreateStudentViewModel StudentModel { get; set; }

    private readonly IConfiguration _config;
    private readonly HttpClient _http;


    public CreateStudent(ILogger<CreateStudent> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      _http = new HttpClient();
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
      var baseUrl = _config.GetValue<string>("baseUrl");
      var url = $"{baseUrl}/students";
      var response = await _http.PostAsJsonAsync(url, StudentModel);
      _http.Dispose();


      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
      }
    }
  }
}
