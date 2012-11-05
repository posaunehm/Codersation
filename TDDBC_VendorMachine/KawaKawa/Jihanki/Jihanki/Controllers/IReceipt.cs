using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Controllers
{

    interface IReceipt
    {
        /// <summary>
        /// お金を受け付ける
        /// </summary>
        /// <param name="money">投入されたお金</param>

        void Receipt(Money.Base.Money money);


    }
}
