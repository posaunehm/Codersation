using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class VendingMachinePresentationModel
    {
        public void SetStock(IEnumerable<Money> moneys)
        {
            throw new System.NotImplementedException();
        }

        public void SetJuice(IEnumerable<Drink> drinks)
        {
            throw new System.NotImplementedException();
        }

        public void InsertMoney(Money money)
        {
            throw new System.NotImplementedException();
        }

        public void BuyDrink(string cola)
        {
            throw new System.NotImplementedException();
        }

        public void PayBack()
        {
            throw new System.NotImplementedException();
        }

        public event EventHandler<MoneyBackedEventArgs> MoneyBacked;

        public void OnMoneyBacked(MoneyBackedEventArgs e)
        {
            var handler = MoneyBacked;
            if (handler != null) handler(this, e);
        }
    }
}