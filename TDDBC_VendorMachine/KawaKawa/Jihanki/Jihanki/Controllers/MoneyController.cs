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
        private Jihanki.Money.Base.Moneys inputMoney = new Jihanki.Money.Base.Moneys();


        /// <summary>
        /// ユーザからの投入金を追加
        /// </summary>
        /// <param name="money">投入金額</param>
        public void InputMoneyAdd(Jihanki.Money.Base.Money money)
        {
            inputMoney.Add(money);
        }

        /// <summary>
        /// ユーザからの投入金総額
        /// </summary>
        public int InputMoneySum()
        {
            return inputMoney.Sum();
        }






    }
}
