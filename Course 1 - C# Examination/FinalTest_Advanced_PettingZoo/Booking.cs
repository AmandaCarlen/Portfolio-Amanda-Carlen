using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalTest_Advanced_PettingZoo
{
    internal class Booking
    {
        public static List<Customer> CurrentLoggedIn = new List<Customer>();
        public static List<Customer> textFileCustomers = new List<Customer>();
        public static List<Goat> textFileGoats = new List<Goat>();
        public static List<Store> textFileStore = new List<Store>();

        public static void RentGoat()
        {
            bool keepRenting = true;
            do
            {
                bool correctInput = false;
                Goat goatToBeRented;
                do
                {
                    OutputHelper.PrintAllAvalibleGoats();
                    goatToBeRented = InputHelper.GetGoatNameAndCheckIfGoatExist("\nPlease enter the name of which goat you'd like to rent:");
                    bool goatToBeRentedIsAvalible = InputHelper.CheckIfGoatIsAvalible(goatToBeRented);
                    if (!goatToBeRentedIsAvalible)
                    {
                        Console.WriteLine($"{goatToBeRented.Name} is not avalible. Press enter to try again or spacebar to exit.");
                        var input = Console.ReadKey().Key;
                        if (input is ConsoleKey.Spacebar)
                        { return; }
                        correctInput = false;
                        Console.Clear();
                    }
                    else
                    { correctInput = true; }
                } while (!correctInput);
                do
                {
                    int numberOfDaysToRent = InputHelper.GetInt("\nFor how many days would you like to rent it?");
                    bool doesUserWantToRentThisGoat = InputHelper.SetBool($"\nAre you sure you want to rent this goat?");
                    if (!doesUserWantToRentThisGoat)
                    {
                        return;
                    }

                    goatToBeRented.DaysRentedFor = numberOfDaysToRent;
                    goatToBeRented.RentedByUserName = CurrentLoggedIn[0].UserName;
                    goatToBeRented.IsAvalible = false;
                    correctInput = true;
                    Console.WriteLine($"\nYou are now renting {goatToBeRented.Name}.");
                    keepRenting = InputHelper.SetBool("\nWould you like to rent another one?");
                } while (!correctInput);
            } while (keepRenting);
        }

        public static void ReturnAGoat()
        {
            bool keepReturning = true;
            do
            {
                OutputHelper.PrintGoatsCurrentlyRentedByLastLoggedIn();
                var rentedByThisUser = textFileGoats.FindAll(goat => goat.RentedByUserName == CurrentLoggedIn[0].UserName);
                if (rentedByThisUser.Count == 0)
                {
                    return;
                }
                Console.WriteLine("\nType the name of the goat you wish to return: ");
                string name = Console.ReadLine().ToLower();
                var goatToReturn = textFileGoats.Find(goat => goat.Name.ToLower() == name && goat.RentedByUserName == Booking.CurrentLoggedIn[0].UserName);

                if (goatToReturn == null)
                {
                    Console.WriteLine($"\nYou're not currently renting a goat named {name}, press enter to try again or spacebar to go exit");
                    var input = Console.ReadKey().Key;
                    if (input is ConsoleKey.Spacebar)
                    { return; }
                }
                else
                {
                    bool doesUserWantToReturnThisGoat = InputHelper.SetBool($"\nAre you sure you want to return this goat?");
                    if (!doesUserWantToReturnThisGoat)
                    {
                        return;
                    }
                    keepReturning = true;
                    int totalBill = Store.CalculateBill(goatToReturn);
                    Console.WriteLine($"\n{goatToReturn.Name} has been returned and you will be billed {totalBill}kr.");
                    InputHelper.CalculateRevenueAsInt(totalBill);
                    goatToReturn.IsAvalible = true;
                    CurrentLoggedIn[0].RentingHistory.Add(goatToReturn.Name);
                    goatToReturn.RentedByUserName = "";
                    goatToReturn.DaysRentedFor = 0;
                }
                keepReturning = InputHelper.SetBool("\nWould you like to return another one?");
            } while (keepReturning);
        }

        public static void CreateANewAccount()
        {
            string firstName = InputHelper.GetNameStringWithOnlyLetters("\nFirst name:");
            string lastName = InputHelper.GetNameStringWithOnlyLetters("\nLast name:");
            string userName = "";
            bool userNameIsTaken = false;
            do
            {
                userName = InputHelper.GetNameStringNoWhiteSpaceNotEmpty("\nUsername:");
                userNameIsTaken = InputHelper.IsUserNameTaken(userName);
            } while (userNameIsTaken);
            string password = InputHelper.GetNameStringNoWhiteSpaceNotEmpty("\nCreate password:");
            var rentingHistory = new List<string>();
            textFileCustomers.Add(new Customer(firstName, lastName, userName, password, rentingHistory));
        }

        public static void LogInToAccount()
        {
            bool validInput = false;
            Customer existingAccount;
            do
            {
                Console.WriteLine("\nUsername:");
                string userName = Console.ReadLine();
                existingAccount = textFileCustomers.Find(customer => customer.UserName == userName);
                if (existingAccount == null || textFileCustomers.Count == 0)
                {
                    Console.WriteLine("\nThere's no such username, press enter to try again or spacebar to go back");
                    var input = Console.ReadKey().Key;
                    if (input is ConsoleKey.Spacebar)
                    { return; }
                }
                else
                    validInput = true;
            } while (!validInput);

            do
            {
                Console.WriteLine("\nPassword:");
                string passWord = Console.ReadLine();
                if (existingAccount.PassWord == passWord)
                {
                    validInput = true;
                    Console.WriteLine($"\nWelcome {existingAccount.FirstName} {existingAccount.LastName}");

                    if (Booking.CurrentLoggedIn.Count > 0)
                    {
                        Booking.CurrentLoggedIn.Remove(Booking.CurrentLoggedIn[0]);
                        Booking.CurrentLoggedIn.Add(existingAccount);
                    }
                    else
                    { Booking.CurrentLoggedIn.Add(existingAccount); }
                    OutputHelper.WaitForUserToPressEnterToFinish();
                }
                else
                {
                    validInput = false;
                    Console.WriteLine("\nWrong password, press enter to try again or spacebar to go back");
                    var input = Console.ReadKey().Key;
                    if (input is ConsoleKey.Spacebar)
                    { return; }
                }
            } while (!validInput);
        }
    }
}