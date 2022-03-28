using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTest_Advanced_PettingZoo
{
    internal class StringListMenuOptions
    {
        public static List<String> StartMenu = new List<String>() { "Admin", "Create account", "Log in", "My account", "Show all goats", "Shut down computer and save changes" };
        public static List<String> AdminMenu = new List<String>() { "Change goat properies", "Remove goat", "Add goat", "Show all goats", "Show revenue", "Back" };
        public static List<String> MyAccountMenu = new List<String>() { "Rent a goat", "Return a goat", "Currently renting", "Renting history", "Back" };
    }
}