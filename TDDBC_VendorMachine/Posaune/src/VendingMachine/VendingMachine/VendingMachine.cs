using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class VendingMachine
    {
        //DrinkStockも作るべきだけれど、この範囲ではLinqの各メソッドで十分なので見送り
        private readonly List<Drink> _drinkStock = new List<Drink>();
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
            _drinkStock.AddRange(drink);
        }

        public Drink BuyDrink(string drinkName)
        {
            var boughtDrink = _drinkStock.FirstOrDefault(
                drink =>
                    drink.Name == drinkName
                    && drink.Price <= TotalAmount
                    && _moneyStocker.CanRetuenJustMoneyIfUsed(drink.Price)
                );
            _moneyStocker.TakeMoney(boughtDrink == null ? 0 : boughtDrink.Price);
            return boughtDrink;
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
