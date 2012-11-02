using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Jihanki.TEST.Money
{
    [TestFixture()]
    class MoneyControllerTest
    {

        //投入金額
        private Jihanki.Money.Base.Money yen10 
            = new Jihanki.Money.Base.Money(Jihanki.Money.Base.MoneyKind.Kind.Yen10);


        
        /// <summary>
        /// 投入するお金ごとの金額指定
        /// </summary>
        /// <param name="num"></param>
         public void SetMoneyNum(int num)
        {
            yen10.Add(num);
        }


         [Test]
         public void 投入金額と投入金額総計が一致するかテスト()
         {
             //投入するお金を用意
             this.SetMoneyNum(1);

             var target = new Jihanki.Money.MoneyController();

             //お金を投入
             target.InputMoneyAdd(this.yen10);

             var expect = 10;
             var actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);




         }


    }
}
