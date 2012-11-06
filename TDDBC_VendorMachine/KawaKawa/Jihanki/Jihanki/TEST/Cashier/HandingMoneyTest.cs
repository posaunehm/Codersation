using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Jihanki.TEST.Cashier
{
         
    [TestFixture]
    class HandingMoneyTest
    {

        [Test]
        public void 取り扱い可能なお金種別であることを確認()
        {
            var target = new Jihanki.Cashier.HandingMoney();

            //10円投下
            var yen 
                = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen10);
            var expect = true;
            var actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);


            //50円投下
            yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen50);
            actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);

            //100円投下
            yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen100);
            actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);


            //500円投下
            yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen500);
            actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);

            //1000円投下
            yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen1000);
            actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);

        }

        [Test]
        public void 取り扱い不可能なお金種別であることを確認()
        {
            var target = new Jihanki.Cashier.HandingMoney();

            //1円投下
            var yen
                = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen1);
            var expect = false;
            var actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);


            //5円投下
            yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen5);
            actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);

            //2000円投下
            yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen2000);
            actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);


            //5000円投下
            yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen5000);
            actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);

            //10000円投下
            yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen10000);
            actual = target.IsHandling(yen);
            Assert.AreEqual(expect, actual);

        }


    }
}
