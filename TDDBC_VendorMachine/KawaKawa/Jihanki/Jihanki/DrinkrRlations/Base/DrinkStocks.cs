using System;
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
    }
}
