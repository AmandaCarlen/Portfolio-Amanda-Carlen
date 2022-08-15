using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using razorApp.ViewModels.Skills;

namespace razorApp.ViewModels.Teacher
{
    public class UpdateTeacherViewModel
    {
         public int Id { get; set; }
         [Required]
        public string? FirstName { get; set; }
         [Required]

        public string? LastName { get; set; }
         [Required]

        public string? PhoneNumber { get; set; }
        public string? StreetAdress { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; } 
        public List<string> Courses {get; set;} = new List<string>();
        public List<string> Skills {get; set;} = new List<string>();
    }
}