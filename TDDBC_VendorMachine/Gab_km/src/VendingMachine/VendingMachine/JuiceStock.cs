using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class JuiceStock
    {
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int Count { get; private set; }

        public JuiceStock(Juice juice, int count)
        {
            this.Name = juice.Name;
            this.Price = juice.Price;
            this.Count = count;
        }

        internal void ReduceCount()
        {
            this.Count--;
        }
    }
}
