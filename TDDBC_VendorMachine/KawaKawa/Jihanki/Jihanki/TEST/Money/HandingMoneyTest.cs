using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.Money;

namespace Jihanki.TEST.Money
{
         
    [TestFixture]
    class HandingMoneyTest
    {

        [TestCase(CurrencyKind.Kind.Yen1, false)]
        [TestCase(CurrencyKind.Kind.Yen5, false)]
        [TestCase(CurrencyKind.Kind.Yen10,true)]
        [TestCase(CurrencyKind.Kind.Yen50, true)]
        [TestCase(CurrencyKind.Kind.Yen100, true)]
        [TestCase(CurrencyKind.Kind.Yen500, true)]
        [TestCase(CurrencyKind.Kind.Yen1000, true)]
        [TestCase(CurrencyKind.Kind.Yen2000, false)]
        [TestCase(CurrencyKind.Kind.Yen5000, false)]
        [TestCase(CurrencyKind.Kind.Yen10000, false)]
        public void お金種別の取り扱い可否を判定テスト(CurrencyKind.Kind kind,bool expected)
        {
            var target = new HandingMoney();

            var yen = new Currency(kind);
            var actual = target.IsHandling(yen);
            Assert.AreEqual(expected, actual);
        }



    }
}
