using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class VendingMachine
    {
        private readonly MoneyStocker _moneyStocker;
        private readonly IDrinkPriceSpecification _drinkPriceSpecification;
        private readonly DrinkStocker _drinkStock;

        public VendingMachine()
            : this(new StandardMoneyAcceptor())
        {
        }

        public VendingMachine(IMoneyAcceptor acceptor)
        {
            _drinkPriceSpecification = new DrinkPriceSpecification();

            _drinkStock = new DrinkStocker();
            _moneyStocker = new MoneyStocker(acceptor);
        }

        public int TotalAmount
        {
            get { return _moneyStocker.InsertedMoneyAmount; }
        }

        public void InsertMoney(Money money)
        {
            _moneyStocker.Insert(money);
        }

        public void AddDrink(IEnumerable<Drink> drink)
        {
            _drinkStock.AddDrinks(drink);
        }

        public Drink BuyDrink(string drinkName)
        {
            if (!CanBuyDrinkNamed(drinkName))
            {
                return null;
            }
            var boughtDrink = _drinkStock.Take(drinkName);
            _moneyStocker.TakeMoney(boughtDrink == null ? 0 : _drinkPriceSpecification.GetItemPrice(drinkName));
            return boughtDrink;
        }

        private bool CanBuyDrinkNamed(string drinkName)
        {
            return _drinkStock.HasItem(drinkName)
                   && _drinkPriceSpecification.GetItemPrice(drinkName) <= TotalAmount
                   && _moneyStocker.CanReturnJustMoneyIfUsed(_drinkPriceSpecification.GetItemPrice(drinkName));
        }

        public IEnumerable<Money> PayBack()
        {
            return _moneyStocker.PayBack();
        }

        public void AddStock(IEnumerable<Money> moneys)
        {
            foreach (var money in moneys)
            {
                _moneyStocker.AddStock(money);
            }
        }

        public void AddStock(IEnumerable<MoneyStockInfo> setMoneyInfos)
        {
            foreach (var money in setMoneyInfos
                .SelectMany(info => Enumerable.Range(1,info.Count).Select(i => new Money(info.Kind))))
            {
                _moneyStocker.AddStock(money);
            }
        }

        public void SetDrinkSpecification(IEnumerable<PriceSpecification> specs)
        {
            foreach (var priceSpecification in specs)
            {
                _drinkPriceSpecification.SetDrinkSpec(priceSpecification);
            }
        }
    }

    public class MoneyStockInfo
    {
        public MoneyStockInfo(MoneyKind kind, int count)
        {
            Kind = kind;
            Count = count;
        }

        public int Count{ get; private set; }

        public MoneyKind Kind { get; private set; }
    }
}
