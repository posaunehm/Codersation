using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using VendingMachine.Model;

namespace VendingMachine.Test {
    [TestFixture]
    public class _釣り銭管理に関するTestSuite {
        [TestCase(Money.Coin1,      500,    false,   0)]
        [TestCase(Money.Coin5,      400,    false,   0)]
        [TestCase(Money.Coin10,     600,    true,    600)]
        [TestCase(Money.Coin50,     1000,   true,    1000)]
        [TestCase(Money.Coin100,    200,    true,    200)]
        [TestCase(Money.Coin500,    700,    true,    700)]
        [TestCase(Money.Bill1000,   20,     false,   0)]
        [TestCase(Money.Bill2000,   2000,   false,   0)]
        [TestCase(Money.Bill5000,   55,     false,   0)]
        [TestCase(Money.Bill10000,  5,      false,   0)]
        public void _釣り銭を補充する(Money inMoney, int inCount, bool inAccepted, int inExpectedCount) {
            var pool = new CreditPool();

            var role = new ChangePoolRole();
            role.Append(pool, inMoney, inCount);

            Assert.That(pool.Credits.Where(c => c.Value > 0).Any(), Is.EqualTo(inAccepted));
            if (inAccepted) {
                Assert.That(pool.Credits[inMoney], Is.EqualTo(inExpectedCount));
            }
        }
    }
}

