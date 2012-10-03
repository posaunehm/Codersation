using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class JuiceGroup
    {
        private Juice juice;
        
        public string Name
        {
            get
            {
                return this.juice.Name;
            }
        }

        public int Price { get; private set; }

        public int Count { get; private set; }

        public JuiceGroup(Juice juice, int price, int count)
        {
            this.juice = juice;
            this.Price = price;
            this.Count = count;
        }

        internal void ReduceCount()
        {
            this.Count--;
        }
    }
}
