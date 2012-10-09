using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class Drink
    {
        public Drink(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public int Price { get; private set; }
        public string Name { get; private set; }
    }
}
