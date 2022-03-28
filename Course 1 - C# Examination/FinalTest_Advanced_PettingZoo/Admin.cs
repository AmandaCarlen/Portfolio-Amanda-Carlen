using System;

namespace FinalTest_Advanced_PettingZoo
{
    internal class Admin
    {
        public const string adminPassWord = "2023";

        public static void CreateAndAddGoat()
        {
            bool keepAddingGoats = false;
            bool nameAvalible = true;
            string name = "";
            do
            {
                do
                {
                    name = InputHelper.GetNameStringWithOnlyLetters("\nName of goat:");
                    var existingGoatname = Booking.textFileGoats.Find(goat => goat.Name.ToLower() == name.ToLower());
                    if (existingGoatname != null)
                    {
                        Console.WriteLine($"\nThere is already a goat named {name}, press enter to try again or spacebar to exit.");
                        var input = Console.ReadKey().Key;
                        if (input is ConsoleKey.Spacebar)
                        { return; }
                        nameAvalible = false;
                    }
                    else
                    { nameAvalible = true; }
                } while (!nameAvalible);
                int age = InputHelper.GetInt("\nAge of goat:");
                bool isAvailable = InputHelper.SetBool("\nIs the goat avalible?");
                bool isSick = InputHelper.SetBool("\nIs the goat sick?");
                int price = InputHelper.GetInt("\nPrice for goat per day:");
                bool doesAdminWantToAddThisGoat = InputHelper.SetBool($"\nAre you sure you want to add this goat?");
                if (!doesAdminWantToAddThisGoat)
                {
                    break;
                }
                Booking.textFileGoats.Add(new Goat(name, age, isAvailable, isSick, price));
                Console.WriteLine($"\n{name} was just added.");
                keepAddingGoats = InputHelper.SetBool("\nWould you like to add another one?");
            } while (keepAddingGoats);
        }

        public static void RemoveGoatByName()
        {
            bool keepRemovingGoats = true;
            do
            {
                OutputHelper.PrintAllGoats();
                Goat goatToBeRemoved = InputHelper.GetGoatNameAndCheckIfGoatExist("\nType the name of the goat you wish to remove");

                if (goatToBeRemoved.RentedByUserName.Length > 0)
                { Console.WriteLine("You can not remove goat that is rented out."); }
                else
                {
                    bool doesAdminWantToRemoveThisGoat = InputHelper.SetBool($"\nAre you sure you want to remove {goatToBeRemoved.Name}?");
                    if (!doesAdminWantToRemoveThisGoat)
                    {
                        break;
                    }
                    Booking.textFileGoats.Remove(goatToBeRemoved);
                    Console.WriteLine($"\n{goatToBeRemoved.Name} was removed");
                }

                keepRemovingGoats = InputHelper.SetBool("\nWould you like to remove another one?");
            } while (keepRemovingGoats);
        }

        public static void ChangeGoatPropertiesByName()
        {
            bool keepChangingProperties = true;
            do
            {
                OutputHelper.PrintAllGoats();
                Goat goatToChange = InputHelper.GetGoatNameAndCheckIfGoatExist("\nType the name of the goat you wish to make changes on");
                Console.WriteLine("\nType which property you'd like to change");
                string property = Console.ReadLine();
                if (property.ToLower() is "name")
                {
                    string newName = InputHelper.GetNameStringWithOnlyLetters("\nWhat do you want it to be called instead?");
                    goatToChange.Name = newName;
                    Console.WriteLine("\nChange was made.");
                    keepChangingProperties = InputHelper.SetBool("\nWould you like to change another one?");
                }
                else if (property.ToLower() is "age")
                {
                    int newAge = InputHelper.GetInt("\nHow old is it now?");
                    goatToChange.Age = newAge;
                    Console.WriteLine("\nChange was made.");
                    keepChangingProperties = InputHelper.SetBool("\nWould you like to change another one?");
                }
                else if (property.ToLower() is "isavalible")
                {
                    bool newIsAvalible = InputHelper.SetBool("\nWhat do you want the availabilty to be? (true/false)?");
                    goatToChange.IsAvalible = newIsAvalible;
                    Console.WriteLine("\nChange was made.");
                    keepChangingProperties = InputHelper.SetBool("\nWould you like to change another one?");
                }
                else if (property.ToLower() is "issick")
                {
                    bool newIsSick = InputHelper.SetBool("\nIs the goat sick? (true/false)?");
                    goatToChange.IsSick = newIsSick;
                    Console.WriteLine("\nChange was made.");
                    keepChangingProperties = InputHelper.SetBool("\nWould you like to change another one?");
                }
                else if (property.ToLower() is "price")
                {
                    int newPrice = InputHelper.GetInt("\nWhat do you want the price to be?");
                    goatToChange.Price = newPrice;
                    Console.WriteLine("\nChange was made.");
                    keepChangingProperties = InputHelper.SetBool("\nWould you like to change another one?");
                }
                else
                {
                    Console.WriteLine($"\nTheres no propety called {property}, press enter to try again or press spacebar to exit");
                    var input = Console.ReadKey().Key;
                    if (input is ConsoleKey.Spacebar)
                    { break; }
                    keepChangingProperties = true;
                }
            } while (keepChangingProperties);
        }

        public static bool LogIn()
        {
            bool logInSuccesfull = false;
            do
            {
                Console.WriteLine("Please type admin password: (Ledtråd för jens, vilket år är denna utbildning klar? )");
                string password = Console.ReadLine();
                if (password != adminPassWord)
                {
                    Console.WriteLine($"\nWrong password, press enter to try again or press spacebar to exit");
                    var input = Console.ReadKey().Key;
                    if (input is ConsoleKey.Spacebar)
                    { return logInSuccesfull; }
                }
                else
                { logInSuccesfull = true; }
            } while (!logInSuccesfull);
            return logInSuccesfull;
        }
    }
}