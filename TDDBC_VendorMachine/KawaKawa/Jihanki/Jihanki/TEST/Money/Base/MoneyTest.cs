using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.Money.Base;

namespace Jihanki.TEST.Money.Base
{
    class MoneyTest
    {


        [Test]
        public void 合計金額計算()
        {

            var target = new Jihanki.Money.Base.Money(1000);

            target.Add(10);

            Assert.AreEqual(target.Sum(), 10000);
        }





    }
}
