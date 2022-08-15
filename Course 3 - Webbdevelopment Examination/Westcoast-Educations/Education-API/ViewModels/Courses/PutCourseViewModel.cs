using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education_API.ViewModels.Courses
{
    public class PutCourseViewModel
    {
        public string? Title { get; set; }
        public string? Length { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int[]? Students {get; set;}
        public int[]? Teachers {get; set;}


    }
}