using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Education_API.ViewModels.Authorization
{
    public class LoginViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]

        public string? Password { get; set; }
    }
}