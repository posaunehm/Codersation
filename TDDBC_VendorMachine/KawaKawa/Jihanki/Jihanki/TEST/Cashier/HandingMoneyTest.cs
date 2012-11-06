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

            //投入するお金
            var yen = new Jihanki.Cashier.Base.Money(Jihanki.Cashier.Base.MoneyKind.Kind.Yen1);


            var expect = true;
            var actual = target.IsHandling(yen);

            Assert.AreEqual(expect, actual);

        }


    }
}
