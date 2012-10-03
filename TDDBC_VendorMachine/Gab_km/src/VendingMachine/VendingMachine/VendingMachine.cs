using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class VendingMachine
    {
        public int Total { get; private set; }
        public List<Money> Change { get; private set; }
        private StockSystem stock;
        public int Sale { get; private set; }
        public List<Money> AllChanges
        {
            get
            {
                return this.stock.AllChanges;
            }
        }

        private List<Money> DroppedInMoney;

        public VendingMachine()
        {
            this.stock = new StockSystem();
            this.Change = new List<Money>();
            this.DroppedInMoney = new List<Money>();
        }

        public void DropIn(Money money)
        {
            if (money.GetType() == typeof(AcceptableMoney))
            {
                this.DroppedInMoney.Add(money);
                this.Total += money.Value;
            }
            else
                this.Change.Add(money);
        }

        public void PayBack()
        {
            var change = this.PayBack(this.Total);
            if (change.IsSome)
            {
                this.Change = change.Value;
                this.Total = 0;
                this.DroppedInMoney.Clear();
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public bool CanBuy(string itemName)
        {
            return new PurchaseSystem(this.stock, this.DroppedInMoney).CanBuy(itemName, this.Total);
        }

        public void Buy(string itemName)
        {
            var purcharseSystem = new PurchaseSystem(this.stock, this.DroppedInMoney);
            var result = purcharseSystem.Buy(itemName, this.Total);

            if (result.Sale > 0)
            {
                this.Sale += result.Sale;
                this.Total = 0;
            }
            this.Change = result.Change;

            this.DroppedInMoney.Clear();
        }

        private Option<List<Money>> PayBack(int total)
        {
            return this.stock.GetChange(total);
        }

        public List<JuiceGroup> GetJuiceStock()
        {
            return this.stock.GetJuiceGroups();
        }
    }
}
