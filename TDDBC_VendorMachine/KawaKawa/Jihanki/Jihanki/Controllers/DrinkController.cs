using Jihanki.DrinkrRlations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Controllers
{
    /// <summary>
    /// ドリンクコントローラ
    /// </summary>
    public class DrinkController
    {
        /// <summary>
        /// 格納しているドリンク
        /// </summary>
        private DrinkStock _drinkList = new DrinkStock();



        /// <summary>
        /// ドリンクの追加
        /// </summary>
        /// <param name="drink"></param>
        internal void Add(Drink drink)
        {
            this._drinkList.Add(drink);
        }



        /// <summary>
        /// 現在格納している本数
        /// </summary>
        /// <returns></returns>

        internal int Count()
        {
            return this._drinkList.Count();
        }


        /// <summary>
        /// 格納している全Drink名称
        /// </summary>
        /// <returns></returns>
        internal List<string> GetListNames()
        {
            return null;
        }

        /// <summary>
        /// 全リスト取得
        /// </summary>
        /// <returns></returns>
        internal List<Drink> AllList()
        {
            return this._drinkList.GetList();
        }
    }
}
