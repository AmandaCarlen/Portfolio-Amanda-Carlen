using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.Models;

namespace Education_API.ViewModels.Teachers
{
    public class PostTeacherViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        // public string? Skills { get; set; }
        public string? StreetAdress { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; } 
        // public int[]? Courses {get; set;}
        public List<string> Skills {get; set;} = new List<string>();
    }
}