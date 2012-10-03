using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class StockSystem
    {
        private JuiceStock juiceStock;
        private ChangeStock changeStock;
        public List<Money> AllChanges
        {
            get
            {
                return this.changeStock.Changes;
            }
        }

        public StockSystem()
        {
            InitializeStock();
            InitializeChangeStock();
        }
        private void InitializeStock()
        {
            this.juiceStock = new JuiceStock();
            this.juiceStock.AddJuices(Juice.NAME_Coke, 120, 5);
            this.juiceStock.AddJuices(Juice.NAME_RedBull, 200, 5);
            this.juiceStock.AddJuices(Juice.NAME_Water, 100, 5);
        }

        private void InitializeChangeStock()
        {
            this.changeStock = new ChangeStock(10, 10, 10, 10, 10);
        }

        public Option<JuiceGroup> GetJuiceGroup(string itemName)
        {
            return juiceStock.GetJuiceGroup(itemName);
        }

        public Option<List<Money>> GetChange(int price)
        {
            return changeStock.GetChange(price);
        }

        public void CommitAllChanges()
        {
            this.changeStock.CommitAll();
        }

        public void RollbackAllChanges()
        {
            this.changeStock.RollbackAll();
        }

        internal List<JuiceGroup> GetJuiceGroups()
        {
            return this.juiceStock.GetJuiceGroups();
        }

        internal void ReduceJuice(string itemName)
        {
            this.juiceStock.ReduceJuices(itemName);
        }
    }
}
