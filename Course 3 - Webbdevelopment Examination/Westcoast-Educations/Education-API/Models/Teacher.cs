using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Education_API.Models
{
    public class Teacher 
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdentityUserId {get; set;}
        [ForeignKey("IdentityUserId")]
        public IdentityUser IdentityUser {get; set;} = new IdentityUser();
        // public string? Skills { get; set; }D
        public string? StreetAdress { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public ICollection<Course> Course {get; set;} = new List<Course>();
        public ICollection<Skill> Skill {get; set;} = new List<Skill>();


    }
}