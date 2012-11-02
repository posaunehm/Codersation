using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.Money.Base;

namespace Jihanki.TEST.Money.Base
{
     [TestFixture]
    class MoneyTest
    {


        [TestCase(MoneyKind.Kind.Yen1,10,10)]
        [TestCase(MoneyKind.Kind.Yen5,10,50)]
        [TestCase(MoneyKind.Kind.Yen10, 10, 100)]
        [TestCase(MoneyKind.Kind.Yen50, 10, 500)]
        [TestCase(MoneyKind.Kind.Yen100, 10, 1000)]
        [TestCase(MoneyKind.Kind.Yen500, 10, 5000)]
        [TestCase(MoneyKind.Kind.Yen1000, 10, 10000)]
        [TestCase(MoneyKind.Kind.Yen2000, 10, 20000)]
        [TestCase(MoneyKind.Kind.Yen10000, 10, 100000)]
        public void 合計金額計算(MoneyKind.Kind kind,int addCount,int sum)
        {

            var target = new Jihanki.Money.Base.Money(kind);

            target.Add(addCount);

            Assert.AreEqual(target.Sum(), sum);
        }





    }
}
