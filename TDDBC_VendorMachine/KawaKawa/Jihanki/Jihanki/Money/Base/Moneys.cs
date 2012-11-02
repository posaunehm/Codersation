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

        /// <summary>
        /// お金を追加
        /// </summary>
        /// <param name="money"></param>
        public void Add(Money money)
        {
            this.stock.Add(money);
        }

        /// <summary>
        /// 合計金額
        /// </summary>
        /// <returns></returns>
        public int Sum()
        {
            var sum = this.stock.Sum(n => n.Sum());

            return sum;
        }


    }
}
