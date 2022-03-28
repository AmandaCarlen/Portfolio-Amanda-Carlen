using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinaltestLibrary.Models
{
    public class Book
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public long ISBN { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public bool IsRented { get; set; } = false;

        public ICollection<BookAuthor> BookAuthor { get; set; } 

        public ICollection<Rent> Rent { get; set; }


    }
}
