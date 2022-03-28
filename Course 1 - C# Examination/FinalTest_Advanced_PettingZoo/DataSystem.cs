using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTest_Advanced_PettingZoo
{
    internal class DataSystem
    {
        public static string questionChooseOption = "\n\nChoose an option by pressing a number on your keyboard...";

        public static void PrintMenu(List<String> options, bool enumerate = true)
        {
            Console.Clear();
            if (options == StringListMenuOptions.StartMenu)
            { Console.WriteLine("\nWelcome to the petting zoo!\n"); }
            Console.CursorVisible = false;

            for (int i = 0; i < options.Count; i++)
            {
                Console.Write(enumerate ? $"\n{i + 1}. {options[i]}" : options[i]);
            }
        }

        public static bool RunProgram()
        {
            bool keepUsingComputer = true;
            ConsoleKey key;
            PrintMenu(StringListMenuOptions.StartMenu);
            Console.WriteLine(questionChooseOption);
            key = Console.ReadKey(true).Key;
            Console.Clear();

            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    bool correctPassword = Admin.LogIn();
                    if (!correctPassword)
                    {
                        return keepUsingComputer = true;
                    }
                    PrintMenu(StringListMenuOptions.AdminMenu);
                    Console.WriteLine(questionChooseOption);
                    key = Console.ReadKey(true).Key;
                    Console.Clear();

                    switch (key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Admin.ChangeGoatPropertiesByName();
                            OutputHelper.WaitForUserToPressEnterToFinish();

                            return keepUsingComputer = true;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Admin.RemoveGoatByName();
                            OutputHelper.WaitForUserToPressEnterToFinish();

                            return keepUsingComputer = true;

                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Admin.CreateAndAddGoat();

                            return keepUsingComputer = true;

                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            OutputHelper.PrintAllGoats();
                            OutputHelper.WaitForUserToPressEnterToFinish();

                            return keepUsingComputer = true;

                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5:
                            Store.PrintRevenue();
                            OutputHelper.WaitForUserToPressEnterToFinish();

                            return keepUsingComputer = true;

                        case ConsoleKey.D6:
                        case ConsoleKey.NumPad6:
                            return keepUsingComputer = true;

                        default:
                            return keepUsingComputer = true;
                    }

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Booking.CreateANewAccount();

                    return keepUsingComputer = true;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Booking.LogInToAccount();

                    return keepUsingComputer = true;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    if (Booking.CurrentLoggedIn.Count == 0)
                    {
                        Console.WriteLine("\nYou have to log in to show an account.");
                        OutputHelper.WaitForUserToPressEnterToFinish();
                        return keepUsingComputer = true;
                    }
                    else { Console.WriteLine($"\nWelcome {Booking.CurrentLoggedIn[0].UserName}"); }
                    PrintMenu(StringListMenuOptions.MyAccountMenu);
                    Console.WriteLine(questionChooseOption);
                    key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Booking.RentGoat();
                            OutputHelper.WaitForUserToPressEnterToFinish();

                            return keepUsingComputer = true;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Booking.ReturnAGoat();
                            OutputHelper.WaitForUserToPressEnterToFinish();

                            return keepUsingComputer = true;

                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            OutputHelper.PrintGoatsCurrentlyRentedByLastLoggedIn();
                            OutputHelper.WaitForUserToPressEnterToFinish();

                            return keepUsingComputer = true;

                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            OutputHelper.PrintCustomerRentingHistory();
                            OutputHelper.WaitForUserToPressEnterToFinish();

                            return keepUsingComputer = true;

                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5:
                            return keepUsingComputer = true;

                        default:
                            return keepUsingComputer = true;
                    }

                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    OutputHelper.PrintAllGoats();
                    OutputHelper.WaitForUserToPressEnterToFinish();

                    break;

                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    return keepUsingComputer = false;

                default:
                    return keepUsingComputer = true;
            }
            return keepUsingComputer = true;
        }
    }
}