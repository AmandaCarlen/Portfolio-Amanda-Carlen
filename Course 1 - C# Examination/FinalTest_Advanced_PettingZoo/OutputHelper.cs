using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTest_Advanced_PettingZoo
{
    class OutputHelper
    {
        public static void PrintAllGoats()
        {
            Console.Clear();
            foreach (Goat goat in Booking.textFileGoats)
            {
                Console.WriteLine($"\nName: {goat.Name}" +
                                  $"\nAge: {goat.Age}" +
                                  $"\nIs available: {goat.IsAvalible}" +
                                  $"\nIs sick: {goat.IsSick}" +
                                  $"\nPrice: {goat.Price}" +
                                  $"\nRented by: {goat.RentedByUserName}");
            }
        }

        public static void PrintAllAvalibleGoats()
        {
            Console.Clear();
            foreach (Goat goat in Booking.textFileGoats)
            {
                if (goat.IsAvalible)
                {
                    Console.WriteLine($"\nName: {goat.Name}" +
                                      $"\nAge: {goat.Age}" +
                                      $"\nIs available: {goat.IsAvalible}" +
                                      $"\nIs sick: {goat.IsSick}" +
                                      $"\nPrice: {goat.Price}");
                }
            }
        }

        public static void WaitForUserToPressEnterToFinish()
        {
            Console.WriteLine("\n\nPress enter to go back to the main menu");
            Console.ReadLine();
        }
        public static void PrintCustomerRentingHistory()
        {
            Console.Clear();
            List<Goat> goatsToPrint = new List<Goat>();
            if (Booking.CurrentLoggedIn[0].RentingHistory.Count() == 0)
            {
                Console.WriteLine($"\nYou ({Booking.CurrentLoggedIn[0].UserName}) have not rented a goat here before.");
                return;
            }

            Console.WriteLine($"\nYour ({Booking.CurrentLoggedIn[0].UserName}) renting history:");
            foreach (string goatName in Booking.CurrentLoggedIn[0].RentingHistory)
            {
                Goat goat = Booking.textFileGoats.Find(goat => goat.Name == goatName);
                var alreadyInList = goatsToPrint.Find(goat => goat.Name == goatName);

                if (goat == null)
                {
                    Console.WriteLine($"{goatName} is no longer with us.");
                }
                else if (alreadyInList != null)
                {
                    //Doesnt add to goatsToPrint so if an animal has been rented more than one from the same user it still wont print more than once
                }
                else
                {
                    goatsToPrint.Add(goat);
                }
            }
            if (goatsToPrint.Count == 0)
            { return; }
            foreach (var goat in goatsToPrint)
            {
                Console.WriteLine($"\nName: {goat.Name}" +
                                       $"\nAge: { goat.Age}" +
                                       $"\nIs Avalible: { goat.IsAvalible}" +
                                       $"\nIs Sick: {goat.IsSick}" +
                                       $"\nPrice: {goat.Price}");
            }
        }
        public static bool PrintGoatsCurrentlyRentedByLastLoggedIn()
        {
            Console.Clear();
            var rentedByThisUser = Booking.textFileGoats.FindAll(goat => goat.RentedByUserName == Booking.CurrentLoggedIn[0].UserName);
            if (rentedByThisUser.Count == 0)
            {
                Console.WriteLine("\nYou are currently not renting any goats");
                return true;
            }
            Console.WriteLine($"\n Goats you ({Booking.CurrentLoggedIn[0].UserName}) are currently renting:");
            foreach (Goat goat in rentedByThisUser)
            {
                Console.WriteLine($"\nName: {goat.Name}" +
                    $"\nAge: { goat.Age}" +
                    $"\nIs Avalible: { goat.IsAvalible}" +
                    $"\nIs Sick: {goat.IsSick}" +
                    $"\nPrice: {goat.Price}" +
                    $"\nRented by: {goat.RentedByUserName}");
            }
            return false;
        }
    }
}
