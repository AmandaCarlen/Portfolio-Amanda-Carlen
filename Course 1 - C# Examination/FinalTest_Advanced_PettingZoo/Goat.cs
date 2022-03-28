using System.Collections.Generic;

namespace FinalTest_Advanced_PettingZoo
{
    internal class Goat
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsAvalible { get; set; }
        public bool IsSick { get; set; }
        public int Price { get; set; }
        public string RentedByUserName { get; set; }
        public int DaysRentedFor { get; set; }

        public Goat(string name, int age, bool isAvalible, bool isSick, int price)
        {
            Name = name;
            Age = age;
            IsAvalible = isAvalible;
            IsSick = isSick;
            Price = price;
        }

        public Goat()
        {
        }
    }
}