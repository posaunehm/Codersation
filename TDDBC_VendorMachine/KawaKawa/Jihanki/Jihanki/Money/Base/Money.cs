using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Money.Base
{
    public class Money
    {
        /// <summary>
        /// 金額種別
        /// </summary>
        private MoneyKind.Kind kind = MoneyKind.Kind.Yen1;

        /// <summary>
        /// 数
        /// </summary>
        private int num = 0;


        public Money(MoneyKind.Kind kind)
        {
            this.kind = kind;
        }



        public void Add(int num)
        {
            this.num = this.num + num;
        }


        /// <summary>
        /// 合計金額
        /// </summary>
        /// <returns></returns>
        public int Sum()
        {
            return (int)(this.kind)*this.num;
        }


    }
}
