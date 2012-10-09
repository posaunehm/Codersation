using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class JuiceStock
    {
        private List<JuiceGroup> juices;

        public JuiceStock()
        {
            this.juices = new List<JuiceGroup>();
        }

        public void AddJuices(string itemName, int price, int count)
        {
            this.juices.Add(new JuiceGroup(new Juice(itemName), price, count));
        }

        public bool CanBuy(string itemName, int price)
        {
            var stock = this.juices.Find(s => s.Name == itemName);
            return stock.Price <= price && stock.Count > 0;
        }

        internal Option<JuiceGroup> GetJuiceGroup(string itemName)
        {
            if (this.juices.Exists(s => s.Name == itemName))
                return Option<JuiceGroup>.Some(this.juices.Find(s => s.Name == itemName));
            else
                return Option<JuiceGroup>.None();
        }

        internal List<JuiceGroup> GetJuiceGroups()
        {
            return this.juices;
        }

        internal void ReduceJuices(string itemName)
        {
            this.juices.Find(j => j.Name == itemName).ReduceCount();
        }
    }
}
