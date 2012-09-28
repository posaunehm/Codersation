using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class JuiceStock
    {
        public Juice Juice { get; private set; }
        
        public string Name
        {
            get
            {
                return this.Juice.Name;
            }
        }

        public int Price
        {
            get
            {
                return this.Juice.Price;
            }
        }

        public int Count { get; private set; }

        public JuiceStock(Juice juice, int count)
        {
            this.Juice = juice;
            this.Count = count;
        }

        internal void ReduceCount()
        {
            this.Count--;
        }
    }
}
