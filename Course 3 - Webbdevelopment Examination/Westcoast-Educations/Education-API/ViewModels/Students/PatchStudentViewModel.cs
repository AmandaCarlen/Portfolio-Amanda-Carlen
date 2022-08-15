using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education_API.ViewModels.Students
{
    public class PatchStudentViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAdress { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public List<string> Courses {get; set;} = new List<string>();
    }
}