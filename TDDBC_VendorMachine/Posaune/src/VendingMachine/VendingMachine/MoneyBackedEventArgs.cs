using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class MoneyBackedEventArgs:EventArgs
    {
        public MoneyBackedEventArgs(IEnumerable<Money> backedMoneyList)
        {
            BackedMoneyList = backedMoneyList.ToList();
        }

        public List<Money> BackedMoneyList { get; private set; }
    }
}