using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Cashier
{
    /// <summary>
    /// 自販機が取り扱う金額種別を判定
    /// </summary>
    public class HandingMoney
    {

        /// <summary>
        /// 取り扱いしているお金かチェック
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public bool IsHandling(Base.Money money)
        {
            return true;
        }



    }
}
