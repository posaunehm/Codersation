using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.Cashier.Base;

namespace Jihanki.TEST.Cashier
{
         
    [TestFixture]
    class HandingMoneyTest
    {

        [TestCase(MoneyKind.Kind.Yen1, false)]
        [TestCase(MoneyKind.Kind.Yen5, false)]
        [TestCase(MoneyKind.Kind.Yen10,true)]
        [TestCase(MoneyKind.Kind.Yen50, true)]
        [TestCase(MoneyKind.Kind.Yen100, true)]
        [TestCase(MoneyKind.Kind.Yen500, true)]
        [TestCase(MoneyKind.Kind.Yen1000, true)]
        [TestCase(MoneyKind.Kind.Yen2000, false)]
        [TestCase(MoneyKind.Kind.Yen5000, false)]
        [TestCase(MoneyKind.Kind.Yen10000, false)]
        public void お金種別の取り扱い可否を判定テスト(MoneyKind.Kind kind,bool expected)
        {
            var target = new Jihanki.Cashier.HandingMoney();

            var yen = new Money(kind);
            var actual = target.IsHandling(yen);
            Assert.AreEqual(expected, actual);
        }



    }
}
