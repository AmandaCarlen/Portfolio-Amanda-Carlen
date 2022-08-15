using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorApp.ViewModels.Student;

namespace razorUserApp.Pages
{
    public class RegisterStudent : PageModel
    {
        private readonly ILogger<RegisterStudent> _logger;
        [BindProperty]
        public CreateStudentViewModel StudentModel { get; set; }

        private readonly IConfiguration _config;
    private readonly HttpClient _http;

        public RegisterStudent(ILogger<RegisterStudent> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public void OnGet()
        {
        }

         public async Task OnPostAsync(){

            using var http = new HttpClient();

            var baseUrl =  _config.GetValue<string>("baseUrl");
            var url = $"{baseUrl}/students";
            var response = await http.PostAsJsonAsync(url, StudentModel);

            if(!response.IsSuccessStatusCode){
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }
        }
    }
}
