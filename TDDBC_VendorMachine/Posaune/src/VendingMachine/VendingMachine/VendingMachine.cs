using System.Collections.Generic;

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
            if (!CheckCanBuyDrinkNamed(drinkName))
            {
                return null;
            }
            var boughtDrink = _drinkStock.Take(drinkName);
            _moneyStocker.TakeMoney(boughtDrink == null ? 0 : _drinkPriceSpecification.GetItemPrice(drinkName));
            return boughtDrink;
        }

        private bool CheckCanBuyDrinkNamed(string drinkName)
        {
            return _drinkStock.HasItem(drinkName)
                   && _drinkPriceSpecification.GetItemPrice(drinkName) <= TotalAmount
                   && _moneyStocker.CanRetuenJustMoneyIfUsed(_drinkPriceSpecification.GetItemPrice(drinkName));
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

        public void SetDrinkSpecification(IEnumerable<PriceSpecification> specs)
        {
            foreach (var priceSpecification in specs)
            {
                _drinkPriceSpecification.SetDrinkSpec(priceSpecification);
            }
        }
    }
}
