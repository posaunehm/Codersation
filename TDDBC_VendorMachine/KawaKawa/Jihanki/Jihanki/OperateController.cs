using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki
{

    public class OperateController
    {
        /// <summary>
        /// お金の受付関係
        /// </summary>
        private ReceiptMoney receiptMoney = new ReceiptMoney();




        /// <summary>
        /// ユーザからの投入金の受付
        /// </summary>
        /// <param name="money">投入金</param>
        public void ReceiptMoney(Money.Base.Money money)
        {
            this.receiptMoney.Receipt(money);
        }



    }
}
