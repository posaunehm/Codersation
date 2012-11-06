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
        private Jihanki.Cashier.Base.Money yen10;
        private Jihanki.Cashier.Base.Money yen50;
        private Jihanki.Cashier.Base.Money yen100;
        private Jihanki.Cashier.Base.Money yen500;


        private void initMoney()
        {       
            yen10= new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen10);
            yen50= new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen50);
            yen100= new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen100);
            yen500= new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen500);


        }

        

        /// <summary>
        /// 投入するお金ごとの金額指定
        /// </summary>
        /// <param name="num"></param>
         public void SetMoneyNum(int num)
        {
            initMoney();

            yen10.Add(num);
            yen50.Add(num);
            yen100.Add(num);
            yen500.Add(num);
        }



        [TestCase(1,10,60,160,660)]
        [TestCase(2,20,120,320,1320)]
        [TestCase(5,50,300,800,3300)]
        public void 投入金額と投入金額総計が一致するかテスト(int insertNum,int exp1,int exp2 ,int exp3,int exp4)
        {
             //投入するお金を用意
             this.SetMoneyNum(insertNum);

             var target = new MoneyController();

             //10円*1を投入
             target.InputMoneyAdd(this.yen10);
             var expect = exp1;
             var actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);

             //50円を投入
             target.InputMoneyAdd(this.yen50);
             expect = exp2;
             actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);

             //100円を投入
             target.InputMoneyAdd(this.yen100);
             expect = exp3;
             actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);

             //500円を投入
             target.InputMoneyAdd(this.yen500);
             expect = exp4;
             actual = target.InputMoneySum();
             Assert.AreEqual(expect, actual);



         }


         
        [TestCase(1,660)]
        [TestCase(2,1320)]
        [TestCase(10,6600)]
        public void 投入金額がそのまま払い戻し金額になるかテスト(int insertNum,int exp)
        {
             //投入するお金を用意
             this.SetMoneyNum(insertNum);

             var target = new MoneyController();

             //お金を投入
             target.InputMoneyAdd(this.yen10);
             target.InputMoneyAdd(this.yen50);
             target.InputMoneyAdd(this.yen100);
             target.InputMoneyAdd(this.yen500);
             
             
             var expect = exp;

             target.MoveInput2Refund();
             
             var actual = target.RefundMoneySum();
             Assert.AreEqual(expect, actual);
             
         }


    }
}
