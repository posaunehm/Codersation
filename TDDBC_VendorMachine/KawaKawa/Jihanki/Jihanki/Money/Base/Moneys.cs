using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Money.Base
{
    public class Moneys
    {
        private List<Money> stock= new List<Money>();


        public void Add(Money money)
        {
            this.stock.Add(money);
        }




    }
}
