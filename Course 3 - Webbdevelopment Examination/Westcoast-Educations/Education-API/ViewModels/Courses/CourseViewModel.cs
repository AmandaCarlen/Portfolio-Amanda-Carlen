using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.Models;
using Education_API.ViewModels.Teachers;

namespace Education_API.ViewModels.Courses
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Length { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public ICollection<StudentViewModel>? Students {get; set;}
        public ICollection<TeacherViewModel>? Teachers {get; set;}


    }
}