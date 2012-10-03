using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class PurchaseResult
    {
        public int Sale { get; private set; }
        public List<Money> Change { get; private set; }

        public PurchaseResult(int sale, List<Money> change)
        {
            this.Sale = sale;
            this.Change = change;
        }
    }
}
