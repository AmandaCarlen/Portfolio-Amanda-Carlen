using System;
using System.Collections.Generic;

namespace FinalTest_Advanced_PettingZoo
{
    internal class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public List<string> RentingHistory { get; set; }

        public Customer(string firstName, string lastName, string userName, string password, List<string> rentingHistory)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            PassWord = password;
            RentingHistory = rentingHistory;
        }

        public Customer()
        {
        }
    }
}