using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using razorApp.ViewModels.Skills;

namespace razorApp.ViewModels.Teacher
{
    public class CreateTeacherViewModel
    {
       [Required(ErrorMessage = "Namnet måste fyllas i")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Efternamet måste fyllas i")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email måste fyllas i")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Lösenord måste fyllas i")]
        public string? PassWord { get; set; }
        [Required(ErrorMessage = "Telefonnummer måste fyllas i")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address måste fyllas i")]
        public string? StreetAdress { get; set; }
        [Required(ErrorMessage = "Postkod måste fyllas i")]
        public string? ZipCode { get; set; }
        [Required(ErrorMessage = "Stad måste fyllas i")]

        public string? City { get; set; }
        [Required(ErrorMessage = "Land måste fyllas i")]

        public string? Country { get; set; } 

        public List<string> Skills {get; set;} = new List<string>();
    }
}