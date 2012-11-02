using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki
{
    class Program
    {
        /// <summary>
        /// お金コントローラー
        /// </summary>
        static Money.MoneyController moneyControl;


        /// <summary>
        /// 自販機の挙動コントローラー
        /// </summary>
        static OperateController operateController;



        static void Main(string[] args)
        {
            moneyControl = new Money.MoneyController();

            operateController = new OperateController(moneyControl);






        }
    }
}
