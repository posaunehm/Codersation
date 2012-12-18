using Jihanki.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Controllers
{
    /// <summary>
    /// お金の管理を行います。
    /// </summary>
    public class MoneyController
    {

        /// <summary>
        /// 投入金額
        /// </summary>
        private Money.Money inputMoney = new Money.Money();

        /// <summary>
        /// 払い戻し金額（おつり）
        /// </summary>
        private Money.Money refundMoney = new Money.Money();



        //----------------------//
        // Clear
        //----------------------//
        /// <summary>
        /// 払い戻し金額初期化
        /// </summary>
        public void RefundMoneyClear()
        {
            this.refundMoney.Clear();
        }



        //----------------------//
        // Add
        //----------------------//
        

        /// <summary>
        /// ユーザからの投入金を追加
        /// </summary>
        /// <param name="money">投入金額</param>
        public void InputMoneyAdd(Currency money)
        {
            //取り扱い可能なお金種別かチェック
            using (var handling = new Money.HandingMoney())
            {
                //取り扱いしているお金種別
                if (handling.IsHandling(money) == true)
                {
                    //投入額に追加
                    inputMoney.Add(money);

                    return;
                }
            }

            //払い戻し金額に追加
            this.refundMoney.Add(money);
        }









        //----------------------//
        // Sum
        //----------------------//





        /// <summary>
        /// ユーザからの投入金総額
        /// </summary>
        public int InputMoneySum()
        {
            return inputMoney.Sum();
        }

        /// <summary>
        /// 払い戻し金額合計
        /// </summary>
        /// <returns></returns>
        public int RefundMoneySum()
        {
            return this.refundMoney.Sum();
        }

        /// <summary>
        /// 投入額を払い戻し金額へ移動
        /// </summary>
        public void MoveInput2Refund()
        {
            var moneyList = this.inputMoney.GetStockList();

            this.refundMoney.Add(moneyList);

            this.inputMoney.Clear();
        }




    }
}
