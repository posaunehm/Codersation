using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki
{

    public class OperateController:IDisposable
    {
        /// <summary>
        /// お金コントローラー
        /// </summary>
        private Money.MoneyController moneyControl;

        /// <summary>
        /// お金の受付関係
        /// </summary>
        private ReceiptMoney receiptMoney = new ReceiptMoney();
       


        public OperateController(Money.MoneyController moneyControl)
        {
            // TODO: Complete member initialization
            this.moneyControl = moneyControl;
        }


        /// <summary>
        /// Event追加
        /// </summary>
        private void addEvent()
        {
            this.receiptMoney.ReceiptEvent += moneyControl.InputMoneyAdd;

        }

        /// <summary>
        /// Event削除
        /// </summary>
        private void removeEvent()
        {
            this.receiptMoney.ReceiptEvent -= moneyControl.InputMoneyAdd;
        }







        /// <summary>
        /// ユーザからの投入金の受付
        /// </summary>
        /// <param name="money">投入金</param>
        public void ReceiptMoney(Money.Base.Money money)
        {
            this.receiptMoney.Receipt(money);
        }








        public void Dispose()
        {
            this.removeEvent();
        }
    }
}
