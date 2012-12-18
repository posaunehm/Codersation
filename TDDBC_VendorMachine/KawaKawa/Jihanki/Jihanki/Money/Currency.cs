using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Money
{
    public class Currency
    {
        /// <summary>
        /// 金額種別
        /// </summary>
        private CurrencyKind.Kind kind = CurrencyKind.Kind.Yen1;

        /// <summary>
        /// 数
        /// </summary>
        private int num = 0;


        public Currency(CurrencyKind.Kind kind)
        {
            this.kind = kind;
        }


        /// <summary>
        /// 金額の種別
        /// </summary>
        /// <returns></returns>
        public CurrencyKind.Kind GetKind()
        {
            return this.kind;
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
