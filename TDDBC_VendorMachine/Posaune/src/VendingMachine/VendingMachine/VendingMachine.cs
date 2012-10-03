namespace VendingMachine
{
    public class VendingMachine
    {
        private MoneyAcceptor _moneyAcceptor = new MoneyAcceptor();

        public VendingMachine()
        {
            TotalAmount = 0;
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
