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

        #region Event

        /// <summary>
        /// 投入金の受付EVENT
        /// </summary>
        private Action<Money.Base.Money> _receiptEvemt;
        public event Action<Money.Base.Money> ReceiptEvent
        {
            add{this._receiptEvemt+=value;}
            remove { this._receiptEvemt -= value; }
        }


        #endregion







        

        /// <summary>
        /// ユーザなどが挿入したお金の受け入れ
        /// </summary>
        /// <param name="money">投入されたお金</param>
        /// <returns>
        /// 
        /// </returns>
        public void Receipt(Money.Base.Money money)
        {
            
            //登録されているユーザ投入金Event分実行
            foreach (Func<Money.Base.Money, bool> n in this._receiptEvemt
                                                           .GetInvocationList()
                                                           .Where(s => s != null))
            {
                n(money);
            }

            
        }
    }
}
