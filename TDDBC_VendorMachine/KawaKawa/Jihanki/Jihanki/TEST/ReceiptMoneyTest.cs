using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MoneyBase=Jihanki.Money.Base;

namespace Jihanki.TEST
{
     [TestFixture]
    class ReceiptMoneyTest
    {

        //お金
        static MoneyBase.Money yen1 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen1);
        static MoneyBase.Money yen5 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen5);
        MoneyBase.Money yen10 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen10);
        MoneyBase.Money yen50 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen50);
        MoneyBase.Money yen100 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen100);
        MoneyBase.Money yen500 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen500);
        MoneyBase.Money yen1000 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen1000);
        static MoneyBase.Money yen2000 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen2000);
        static MoneyBase.Money yen5000 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen5000);
        static MoneyBase.Money yen10000 = new MoneyBase.Money(MoneyBase.MoneyKind.Kind.Yen1000);



        [TestFixtureSetUp]
        public void Init()
        {
            //------------------------//
            //お金の金額を指定
            //------------------------//
            yen10.Add(1);
            yen50.Add(1);
            yen100.Add(1);
            yen500.Add(1);
            yen1000.Add(1);

        }


        // [TestCase]
        //public void お金の投入10円()
        //{
        //    var target = new ReceiptMoney();
        //    bool expected = true;

        //    bool actual;
        //    actual = target.Receipt(this.yen10);

        //    Assert.AreEqual(expected, actual);
        //}




        // [Test]
        // public void お金の投入50円()
        // {
        //     var target = new ReceiptMoney();
        //     bool expected = true;

        //     bool actual;
        //     actual = target.Receipt(this.yen50);

        //     Assert.AreEqual(expected, actual);
        // }



        // [Test]
        // public void お金の投入100円()
        // {
        //     var target = new ReceiptMoney();
        //     bool expected = true;

        //     bool actual;
        //     actual = target.Receipt(this.yen100);

        //     Assert.AreEqual(expected, actual);
        // }



        // [Test]
        // public void お金の投入500円()
        // {
        //     var target = new ReceiptMoney();
        //     bool expected = true;

        //     bool actual;
        //     actual = target.Receipt(this.yen500);

        //     Assert.AreEqual(expected, actual);
        // }


        // [Test]
        // public void お金の投入1000円()
        // {
        //     var target = new ReceiptMoney();
        //     bool expected = true;

        //     bool actual;
        //     actual = target.Receipt(this.yen1000);

        //     Assert.AreEqual(expected, actual);
        // }

    }
}
