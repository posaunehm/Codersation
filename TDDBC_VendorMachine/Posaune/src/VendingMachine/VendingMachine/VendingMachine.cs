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

        public void AddDrink(Drink drink)
        {
            _drinkStock.Add(drink);
        }

        public Drink BuyDrink(Drink drink)
        {
            if (TotalAmount >= drink.Price)
            {
                TotalAmount -= drink.Price;
                return drink;
            }
            else return null;
        }

        public Drink BuyDrink(string drinkName)
        {
            var boughtDrink = _drinkStock.FirstOrDefault(drink => drink.Name == drinkName && drink.Price <= TotalAmount);
            TotalAmount -= boughtDrink == null ? 0 : boughtDrink.Price;
            return boughtDrink;
        }

        public List<Money> PayBack()
        {
            var returnMoney = new List<Money>();

            foreach (
                var moneyKind in
                    new List<MoneyKind>
                        {MoneyKind.Yen500, MoneyKind.Yen100, MoneyKind.Yen50, MoneyKind.Yen10})
            {
                while (true)
                {
                    var moneyTemp = new Money(moneyKind);
                    if (moneyTemp.Amount <= TotalAmount)
                    {
                        returnMoney.Add(moneyTemp);
                        TotalAmount -= moneyTemp.Amount;
                    }
                    else{break;}
                }
            }

            return returnMoney;
        }
    }
}
