using System;
using System.ComponentModel.DataAnnotations;

namespace FinaltestLibrary.Models
{
    public class Rent
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public int LibraryCardID { get; set; }
        public int BookID { get; set; }
        public DateTime? RentedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public Book Book { get; set; }
        public LibraryUser LibraryCard { get; set; }

    }
}
