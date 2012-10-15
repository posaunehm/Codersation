using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace VendingMachine
{
    public class VendingMachine
    {
        private readonly IMoneyAcceptor _moneyAcceptor;
        private readonly List<Drink> _drinkStock = new List<Drink>();
        private readonly List<Money> _moneyStock = new List<Money>();

        public VendingMachine(IMoneyAcceptor acceptor)
        {
            TotalAmount = 0;
            _moneyAcceptor = acceptor;
        }

        public int TotalAmount { get; private set; }

        public void InsertMoney(Money money)
        {
            if(_moneyAcceptor.IsValid(money))
            {
                TotalAmount += money.Amount;
            }
        }

        public void AddDrink(IEnumerable<Drink> drink)
        {
            _drinkStock.AddRange(drink);
        }

        public Drink BuyDrink(string drinkName)
        {
            var boughtDrink = _drinkStock.FirstOrDefault(
                drink =>
                drink.Name == drinkName
                && drink.Price <= TotalAmount
                && CanReturnMoney(TotalAmount - drink.Price));
            TotalAmount -= boughtDrink == null ? 0 : boughtDrink.Price;
            return boughtDrink;
        }

        public List<Money> PayBack()
        {
            var returnMoney = new List<Money>();
            var amount = this.TotalAmount;
            foreach (var money in _moneyStock.OrderByDescending(m => m.Amount))
            {
                if ((amount - money.Amount) >= 0)
                {
                    amount -= money.Amount;
                    returnMoney.Add(money);
                }
                if (amount == 0)
                {
                    break;
                }
            }
            if(amount != 0)
            {throw new ApplicationException("VendingMachine couldn't prepare return money");}
            TotalAmount = 0;
            return returnMoney;
        }

        private bool CanReturnMoney(int amount)
        {
            foreach (var money in _moneyStock.OrderByDescending(m => m.Amount))
            {
                if ((amount - money.Amount) >= 0)
                {
                    amount -= money.Amount;
                }
                if (amount == 0)
                {
                    break;
                }
            }
            return amount == 0;
        }

        public void SetStock(IEnumerable<Money> moneys)
        {
            _moneyStock.AddRange(moneys);
        }
    }
}
