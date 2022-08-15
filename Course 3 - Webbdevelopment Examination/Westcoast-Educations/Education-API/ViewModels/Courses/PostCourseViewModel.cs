using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Education_API.ViewModels.Courses
{
    public class PostCourseViewModel
    {
        [Required]
        [Range(1000,9999, ErrorMessage = "Number must be between 1000-9999")]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Length { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int[]? Students {get; set;}
        public int[]? Teachers {get; set;}


    }
}