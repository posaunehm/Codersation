using System.Collections.Generic;

namespace VendingMachine
{
    public class VendingMachine
    {
        private readonly DrinkStocker _drinkStock = new DrinkStocker();
        private readonly MoneyStocker _moneyStocker;


        public VendingMachine()
            : this(new StandardMoneyAcceptor())
        {
        }

        public VendingMachine(IMoneyAcceptor acceptor)
        {
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
            if (!CheckCanBuyDrinkNamed(drinkName))
            {
                return null;
            }
            var boughtDrink = _drinkStock.Take(drinkName);
            _moneyStocker.TakeMoney(boughtDrink == null ? 0 : boughtDrink.Price);
            return boughtDrink;
        }

        private bool CheckCanBuyDrinkNamed(string drinkName)
        {
            return _drinkStock.HasItem(drinkName)
                   && _drinkStock.GetItemPrice(drinkName) <= TotalAmount
                   && _moneyStocker.CanRetuenJustMoneyIfUsed(_drinkStock.GetItemPrice(drinkName));
        }

        public IEnumerable<Money> PayBack()
        {
            return _moneyStocker.PayBack();
        }

        public void SetStock(IEnumerable<Money> moneys)
        {
            foreach (var money in moneys)
            {
                _moneyStocker.Stock(money);
            }
        }
    }
}
