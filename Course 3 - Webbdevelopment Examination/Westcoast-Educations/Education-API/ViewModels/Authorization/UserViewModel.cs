using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education_API.ViewModels.Authorization
{
    public class UserViewModel
    {
        public string? UserName { get; set; }
        // Ta bort denna?
    public DateTime Expires { get; set; }
    public string? Token { get; set; }
    }
}