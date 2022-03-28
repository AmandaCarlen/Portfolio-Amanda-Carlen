using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalTest_Advanced_PettingZoo
{
    internal class InputHelper
    {
        public static string GetNameStringWithOnlyLetters(string question)
        {
            bool inputCorrect = false;
            string input = "";
            do
            {
                Console.WriteLine(question);
                input = Console.ReadLine();
                if (input.Length == 0 || input.Contains(" ") || !Regex.IsMatch(input, @"^[a-zA-ZåäöÅÄÖ]*$"))
                {
                    Console.WriteLine("Only allowed characters for a name is A-Ö.");
                    inputCorrect = false;
                }
                else if (Regex.IsMatch(input, @"^[a-zA-ZåäöÅÄÖ]*$"))
                {
                    inputCorrect = true;
                }
            } while (!inputCorrect);
            return input;
        }

        public static string GetNameStringNoWhiteSpaceNotEmpty(string question)
        {
            bool inputCorrect = false;
            string input = "";
            while (!inputCorrect)
            {
                Console.WriteLine(question);
                input = Console.ReadLine();
                if (input.Length == 0 || input.Contains(" "))
                {
                    Console.WriteLine("You can not leave this field empty or use space.");
                    inputCorrect = false;
                }
                else
                {
                    inputCorrect = true;
                }
            }
            return input;
        }

        public static bool IsUserNameTaken(string username)
        {
            bool usernameTaken = false;

            var existingUsername = Booking.textFileCustomers.Find(customer => customer.UserName == username);
            if (existingUsername != null)
            {
                Console.WriteLine("\nThat username is already taken. Please try again.");
                usernameTaken = true;
            }
            else
                usernameTaken = false;

            return usernameTaken;
        }

        public static bool SetBool(string question)
        {
            do
            {
                Console.WriteLine(question);
                string input = Console.ReadLine();
                if (input.ToLower() == "true" || input.ToLower() == "yes")
                { return true; }
                else if (input.ToLower() == "false" || input.ToLower() == "no")
                { return false; }
                else
                { Console.WriteLine("\nThe only correct answer is true/false and yes/no. Please try again."); }
            } while (true);
        }

        public static Goat GetGoatNameAndCheckIfGoatExist(string question)
        {
            bool correctInput = false;
            Goat goat;
            do
            {
                Console.WriteLine(question);
                string userInput = Console.ReadLine().ToLower();
                goat = Booking.textFileGoats.Find(goat => goat.Name.ToLower() == userInput);
                if (goat != null)
                    correctInput = true;
                else
                {
                    Console.WriteLine($"\nThere is no goat named {userInput}, please try again.");
                }
            } while (!correctInput);
            return goat;
        }

        public static bool CheckIfGoatIsAvalible(Goat goat)

        {
            if (goat.IsAvalible == true)
                return true;
            else
                return false;
        }

        public static int GetInt(string question)
        {
            bool correctInput = false;
            int number = 0;
            do
            {
                Console.WriteLine(question);
                correctInput = int.TryParse(Console.ReadLine(), out number);
                if (correctInput && number > 0)
                { correctInput = true; }
                else
                {
                    Console.WriteLine("You did not type a number thats bigger than 0. Please try again.");
                    correctInput = false;
                }
            } while (!correctInput);
            return number;
        }

        public static void CalculateRevenueAsInt(int num)
        {
            Booking.textFileStore[0].Revenue += num;
        }
    }
}