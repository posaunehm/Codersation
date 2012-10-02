using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class VendingMachine
    {
        public VendingMachine()
        {
            TotalAmount = 0;
        }

        public int TotalAmount { get; private set; }

        public void InsertMoeny(int amount)
        {
            if (amount == 10 || amount == 50 || amount == 100 || amount == 500 || amount == 1000)
            TotalAmount += amount;
        }

        public void AddDrink(Drink drink)
        {
            
        }

        public  Drink BuyDrink(Drink drink)
        {
            return drink;
        }
    }
}
