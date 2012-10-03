using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class PurchaseSystem
    {
        private StockSystem stock;
        private List<Money> droppedInMoney;

        public PurchaseSystem(StockSystem stock, List<Money> droppedInMoney)
        {
            this.stock = stock;
            this.droppedInMoney = droppedInMoney;
        }

        public bool CanBuy(string itemName, int price)
        {
            var maybeJuiceGroup = this.stock.GetJuiceGroup(itemName);
            if (maybeJuiceGroup.IsSome)
            {
                var juiceGroup = maybeJuiceGroup.Value;
                return juiceGroup.Price <= price && juiceGroup.Count > 0;
            }
            else
                return false;
        }

        public PurchaseResult Buy(string itemName, int price)
        {
            if (this.CanBuy(itemName, price))
            {
                var juices = stock.GetJuiceGroup(itemName).Value;
                var change = this.stock.GetChange(price - juices.Price);
                if (change.IsSome)
                    return BuySomething(juices, change);
                else
                    return BuyNothing();
            }
            else
                return BuyNothing();
        }

        private PurchaseResult BuyNothing()
        {
            this.stock.RollbackAllChanges();
            return new PurchaseResult(0, new List<Money>(this.droppedInMoney));
        }

        private PurchaseResult BuySomething(JuiceGroup juices, Option<List<Money>> change)
        {
            this.stock.ReduceJuice(juices.Name);
            this.stock.CommitAllChanges();
            return new PurchaseResult(juices.Price, change.Value);
        }
    }
}
