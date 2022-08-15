using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.Models;
using Education_API.ViewModels.Courses;
using Education_API.ViewModels.Skills;

namespace Education_API.ViewModels.Teachers
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
        public ICollection<CourseViewModel>? Course {get; set;}
        public ICollection<SkillViewModel>? Skill {get; set;}

    }
}