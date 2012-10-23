using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class Drink
    {
        public Drink(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
