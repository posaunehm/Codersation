using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki
{

    interface IReceipt
    {
        /// <summary>
        /// お金を受け付ける
        /// </summary>
        /// <param name="money">投入されたお金</param>
        /// <returns>受付結果
        /// true･･･成功
        /// false･･･失敗
        /// </returns>
        bool Receipt(Money.Base.Money money);


    }
}
