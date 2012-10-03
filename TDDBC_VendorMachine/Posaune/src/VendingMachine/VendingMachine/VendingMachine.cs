namespace VendingMachine
{
    public class VendingMachine
    {
        private readonly IMoneyAcceptor _moneyAcceptor;

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
            
        }

        public Drink BuyDrink(Drink drink)
        {
            return drink;
        }
    }
}
