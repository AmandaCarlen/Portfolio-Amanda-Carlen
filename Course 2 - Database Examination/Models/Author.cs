using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinaltestLibrary.Models
{
    public class Author
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public ICollection<BookAuthor> BookAuthor { get; set; }
    }
}
