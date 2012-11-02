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
        /// <param name="money"></param>
        void Receipt(Money.Base.Money money);


    }
}
