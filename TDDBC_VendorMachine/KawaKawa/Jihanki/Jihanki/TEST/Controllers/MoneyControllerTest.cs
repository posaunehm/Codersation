using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.Controllers;
using Jihanki.Cashier.Base;



namespace Jihanki.TEST.Controllers
{
    [TestFixture()]
    class MoneyControllerTest
    {

        //投入金額
        private Jihanki.Cashier.Base.Money yen10
            = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen10);

        private Jihanki.Cashier.Base.Money yen50
                    = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen50);


        private Jihanki.Cashier.Base.Money yen100
            = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen100);

        private Jihanki.Cashier.Base.Money yen500
            = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen500);

        


        /// <summary>
        /// 投入するお金ごとの金額指定
        /// </summary>
        /// <param name="num"></param>
         public void SetMoneyNum(int num)
        {
            yen10.Add(num);
            yen50.Add(num);
            yen100.Add(num);
            yen500.Add(num);
        }


         [Test]
         public void 投入金額と投入金額総計が一致するかテスト()
         {
             //投入するお金を用意
             this.SetMoneyNum(1);

             var target = new MoneyController();

             //10円*1を投入
             target.InputMoneyAdd(this.yen10);
             var expect = 10;
             var actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);

             //50円*1を投入
             target.InputMoneyAdd(this.yen50);
             expect = 60;
             actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);

             //100円*1を投入
             target.InputMoneyAdd(this.yen100);
             expect = 160;
             actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);

             //500円*1を投入
             target.InputMoneyAdd(this.yen500);
             expect = 660;
             actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);



         }


    }
}
