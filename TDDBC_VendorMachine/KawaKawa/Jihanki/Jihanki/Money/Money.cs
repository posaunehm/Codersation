using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Money
{
    public class Money
    {
        private List<Currency> stock= new List<Currency>();

        /// <summary>
        /// お金を追加
        /// </summary>
        /// <param name="money"></param>
        public void Add(Currency money)
        {
            this.stock.Add(money);
        }
        public void Add(List<Currency> moneyList)
        {
            this.stock.AddRange(moneyList);
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


        /// <summary>
        /// お金の初期化
        /// </summary>
        public void Clear()
        {
            this.stock.Clear();
        }

        /// <summary>
        /// 現在のストックリストを取得
        /// </summary>
        /// <returns></returns>
        public List<Currency> GetStockList()
        {
            return this.stock;
        }


    }
}
