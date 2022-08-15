using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using razorApp.ViewModels.Skills;

namespace razorApp.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAdress { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; } 
        public ICollection<CourseViewModel>? Courses {get; set;}
        public ICollection<SkillViewModel>? Skills {get; set;}

    }
}