using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki
{
    /// <summary>
    /// 挿入されたお金の受け入れ
    /// </summary>
    public class ReceiptMoney:IReceipt
    {

        /// <summary>
        /// ユーザなどが挿入したお金の受け入れ
        /// </summary>
        /// <param name="money"></param>
        public bool Receipt(Money.Base.Money money)
        {
            return true;
        }
    }
}
