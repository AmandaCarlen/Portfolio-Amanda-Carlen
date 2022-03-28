using System;
using System.Collections.Generic;

namespace FinalTest_Advanced_PettingZoo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string goatFile = @"GoatFile.txt";
            string customerFile = @"CustomerFile.txt";
            string storeFile = @"StoreFile.txt";

            Booking.textFileGoats = TextFileProcessor.LoadFromTextFile<Goat>(goatFile);
            Booking.textFileCustomers = TextFileProcessor.LoadFromTextFile<Customer>(customerFile);
            Booking.textFileStore = TextFileProcessor.LoadFromTextFile<Store>(storeFile);

            bool keepRunningProgram = true;
            do
            {
                keepRunningProgram = DataSystem.RunProgram();
            } while (keepRunningProgram);

            TextFileProcessor.SaveToTextFile<Goat>(Booking.textFileGoats, goatFile);
            TextFileProcessor.SaveToTextFile<Customer>(Booking.textFileCustomers, customerFile);
            TextFileProcessor.SaveToTextFile<Store>(Booking.textFileStore, storeFile);
        }
    }
}