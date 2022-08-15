using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace razorApp.ViewModels
{
    public class CreateCourseViewModel
    {
        [Required]
        [Range(1000,9999, ErrorMessage = "Number must be between 1000-9999")]
        [Display(Name = "Kursnummer")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Längd")]
        public string? Length { get; set; }

        [Display(Name = "Kategori")]
        public string? Category { get; set; }

        [Display(Name = "Beskriving")]
        public string? Description { get; set; }

        [Display(Name = "Detaljer")]
        public string? Details { get; set; }

        [Display(Name = "Studenter")]
        public int[]? Students {get; set;}

        [Display(Name = "Lärare")]
        public int[]? Teachers {get; set;}
    }
}