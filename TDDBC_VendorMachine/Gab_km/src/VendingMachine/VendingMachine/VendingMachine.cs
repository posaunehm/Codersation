using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class VendingMachine
    {
        public int Total { get; private set; }
        public int Change { get; private set; }
        public List<JuiceStock> Stock { get; set; }
        public int Sale { get; private set; }

        public VendingMachine()
        {
            this.Stock = new List<JuiceStock>();
            this.Stock.Add(new JuiceStock(new Juice(Juice.NAME_Coke, 120), 5));
            this.Stock.Add(new JuiceStock(new Juice(Juice.NAME_RedBull, 200), 5));
            this.Stock.Add(new JuiceStock(new Juice(Juice.NAME_Water, 100), 5));
        }

        public void DropIn(Money money)
        {
            if (money.GetType() == typeof(AcceptableMoney))
                this.Total += money.Value;
            else
                this.Change += money.Value;
        }

        public void PayBack()
        {
            this.Change = this.Total;
            this.Total = 0;
        }

        public bool CanBuy(string itemName)
        {
            var stock = this.Stock.Find(s => s.Name == itemName);
            return stock.Price <= this.Total && stock.Count > 0;
        }

        public void Buy(string itemName)
        {
            var stock = this.Stock.Find(Stock => Stock.Name == itemName);
            if (stock.Price <= this.Total)
            {
                this.Sale += stock.Price;
                this.Total -= stock.Price;
                stock.ReduceCount();
            }
        }
    }
}
