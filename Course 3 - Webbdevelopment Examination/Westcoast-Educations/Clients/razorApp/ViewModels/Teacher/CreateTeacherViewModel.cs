using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using razorApp.ViewModels.Skills;

namespace razorApp.ViewModels.Teacher
{
    public class CreateTeacherViewModel
    {
        [Required(ErrorMessage = "Namnet måste fyllas i")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Efternamet måste fyllas i")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email måste fyllas i")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Lösenord måste fyllas i")]
        public string? PassWord { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? StreetAdress { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; } 
        // public ICollection<CourseViewModel>? Courses {get; set;}
        public List<string> Skills {get; set;} = new List<string>();
    }
}