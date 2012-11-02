using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Money
{
    /// <summary>
    /// お金の管理を行います。
    /// </summary>
    public class MoneyController
    {

        /// <summary>
        /// 投入金額
        /// </summary>
        private Base.Moneys inputMoney = new InputMoney();


        /// <summary>
        /// ユーザからの投入金を追加
        /// </summary>
        /// <param name="money">投入金額</param>
        public void InputMoneyAdd(Base.Money money)
        {
            inputMoney.Add(money);
        }






    }
}
