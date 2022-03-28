using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinaltestLibrary.Models
{
    public class LibraryUser
    {
        [Key]
        public int LibraryCardID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public ICollection<Rent> Rent { get; set; } = new List<Rent>();

    }
}
