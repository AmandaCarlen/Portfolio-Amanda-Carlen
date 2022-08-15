using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Education_API.Models
{
    public class Course
    {
        [Required]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Length { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = new Category();

        public ICollection<Teacher> Teacher {get; set;} = new List<Teacher>();
        public ICollection<Student> Student {get; set;} = new List<Student>();

       


    }
}