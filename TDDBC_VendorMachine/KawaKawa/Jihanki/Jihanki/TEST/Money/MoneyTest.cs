using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.Money;

namespace Jihanki.TEST.Moneya
{
     [TestFixture]
    class MoneyTest
    {


        [TestCase(CurrencyKind.Kind.Yen1,10,10)]
        [TestCase(CurrencyKind.Kind.Yen5,10,50)]
        [TestCase(CurrencyKind.Kind.Yen10, 10, 100)]
        [TestCase(CurrencyKind.Kind.Yen50, 10, 500)]
        [TestCase(CurrencyKind.Kind.Yen100, 10, 1000)]
        [TestCase(CurrencyKind.Kind.Yen500, 10, 5000)]
        [TestCase(CurrencyKind.Kind.Yen1000, 10, 10000)]
        [TestCase(CurrencyKind.Kind.Yen2000, 10, 20000)]
        [TestCase(CurrencyKind.Kind.Yen10000, 10, 100000)]
        public void 金額種別ごとの合計金額計算(CurrencyKind.Kind kind,int addCount,int sum)
        {
            var target = new Currency(kind);
            target.Add(addCount);
            Assert.AreEqual(target.Sum(), sum);
        }





    }
}
