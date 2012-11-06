using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.DrinkrRlations.Base
{
    public class DrinkStocks
    {
        private List<Drink> stocks = new List<Drink>();




        /// <summary>
        /// ドリンクストックの追加
        /// </summary>
        /// <param name="drink"></param>
        internal void Add(Drink drink)
        {
            this.stocks.Add(drink);
        }

        /// <summary>
        /// ストック数を取得
        /// </summary>
        /// <returns></returns>
        internal int Count()
        {
            return this.stocks.Count();
        }

        /// <summary>
        /// 全リスト取得
        /// </summary>
        /// <returns></returns>
        internal List<Drink> GetList()
        {
            return this.stocks;
        }

    }
}
