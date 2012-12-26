using Jihanki.Money;
using Jihanki.DrinkrRlations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Controllers

{

    public class MainController:IDisposable
    {
        /// <summary>
        /// お金コントローラー
        /// </summary>
        private MoneyController moneyControl=new MoneyController();

        /// <summary>
        /// お金の受付関係
        /// </summary>
        private ReceiptMoney receiptMoney = new ReceiptMoney();

        /// <summary>
        /// ドリンク操作
        /// </summary>
        private DrinkController drinkControl = new DrinkController();


        public MainController()
        {
            //Event設定
            addEvent();
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
        public void ReceiptMoney(Currency money)
        {
            this.receiptMoney.Receipt(money);
        }


        /// <summary>
        /// ドリンクを補充
        /// </summary>
        /// <param name="drink"></param>
        public void AddDrink(Drink drink)
        {
            this.drinkControl.Add(drink);
        }


        /// <summary>
        /// ドリンクが購入可能かチェック
        /// </summary>
        /// <returns></returns>
        internal bool IsBuy()
        {
            //投入金額を取得
            var insertMoney = this.moneyControl.InputMoneySum();

            //ドリンクの単価を取得
            var price = this.drinkControl.AllList()
                                         .Select(n => n.Price())
                                         .FirstOrDefault();

            if (insertMoney >= price)
            {
                return true;
            }
            
            return false;
        }



        public void Dispose()
        {
            this.removeEvent();
        }

    }
}
