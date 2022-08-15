using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Education_API.ViewModels.Authorization
{
  public class RegisterUserViewModel
  {
    [Required]
    [EmailAddress(ErrorMessage = "Felaktig e-post adress")]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsAdmin { get; set; } = false;
    public bool IsTeacher {get; set;} = false;
    public bool IsStudent {get; set;} = false;
  }
}