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

        public void InsertMoney(Money money)
        {
            switch (money.Kind)
            {
                case MoneyKind.Yen10:
                case MoneyKind.Yen50:
                case MoneyKind.Yen100:
                case MoneyKind.Yen500:
                case MoneyKind.Yen1000:
                    TotalAmount += money.Amount;
                    break;
                case MoneyKind.Yen1:
                case MoneyKind.Yen5:
                case MoneyKind.Yen5000:
                case MoneyKind.Yen10000:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


        }

        public void AddDrink(Drink drink)
        {
            
        }

        public Drink BuyDrink(Drink drink)
        {
            return drink;
        }
    }
}
