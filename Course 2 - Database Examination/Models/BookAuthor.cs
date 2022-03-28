using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinaltestLibrary.Models
{
    public class BookAuthor
    {
       
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public Book Book { get; set; }
        public Author Author { get; set; }


    }
}
