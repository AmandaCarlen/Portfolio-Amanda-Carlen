using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTest_Advanced_PettingZoo
{
    internal class Store
    {
        public int Revenue { get; set; }

        public Store(int revenue)
        {
            Revenue = revenue;
        }

        public Store()
        {
        }

        public static int CalculateBill(Goat goat)
        {
            int bill = goat.DaysRentedFor * goat.Price;
            return bill;
        }

        public static void PrintRevenue()
        {
            Console.WriteLine($"The total revenue is {Booking.textFileStore[0].Revenue}kr.");
        }
    }
}