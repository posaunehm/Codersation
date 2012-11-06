using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.DrinkrRlations.Base
{
    public class Drink
    {
        private int price;
        private string name;


        public Drink(int price, string name)
        {
            this.price = price;
            this.name = name;
        }





        /// <summary>
        /// 値段を取得
        /// </summary>
        /// <returns></returns>
        internal int Price()
        {
            return this.price;
        }

        /// <summary>
        /// 名前を取得
        /// </summary>
        /// <returns></returns>
        internal string Name()
        {
            return "コーラ";
        }
    }
}
