using System.Collections.Generic;
using System.Linq;
using VendingMachine.Domain;

namespace VendingMachine.PresentationModel.EventArgs
{
    public class MoneyBackedEventArgs:System.EventArgs
    {
        public MoneyBackedEventArgs(IEnumerable<Money> backedMoneyList)
        {
            BackedMoneyList = backedMoneyList.ToList();
        }

        public List<Money> BackedMoneyList { get; private set; }
    }
}