using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using VendingMachine.Model;

namespace VendingMachine.Test {
    [TestFixture]
    public class _釣り銭管理に関するTestSuite {
        [TestCase(Money.Coin10,     600)]
        [TestCase(Money.Coin50,     1000)]
        [TestCase(Money.Coin100,    200)]
        [TestCase(Money.Coin500,    700)]
        public void _釣り銭を補充する(Money inMoney, int inCount) {
            var pool = new CreditPool();

            var role = new ChangePoolRole();
            var newPool = role.Append(pool, inMoney, inCount);

            Assert.That(newPool.Credits.Where(c => c.Value > 0).Any(), Is.True);
            Assert.That(newPool.Credits[inMoney], Is.EqualTo(inCount));
        }

        [TestCase(Money.Coin1,      500)]
        [TestCase(Money.Coin5,      400)]
        [TestCase(Money.Bill1000,   20 )]
        [TestCase(Money.Bill2000,   2000)]
        [TestCase(Money.Bill5000,   55 )]
        [TestCase(Money.Bill10000,  5  )]
        public void _釣り銭を補充する_対象外金種の場合(Money inMoney, int inCount) {
            var pool = new CreditPool();
            
            var role = new ChangePoolRole();
            var newPool = role.Append(pool, inMoney, inCount);
            
            Assert.That(newPool.Credits.Where(c => c.Value > 0).Any(), Is.False);
        }

        [TestCase(Money.Coin10,     -600)]
        [TestCase(Money.Coin50,     -1000)]
        [TestCase(Money.Coin100,    -30)]
        [TestCase(Money.Coin500,    -700)]
        [TestCase(Money.Coin1,      -500)]
        [TestCase(Money.Coin5,      -400)]
        [TestCase(Money.Bill1000,   -20 )]
        [TestCase(Money.Bill2000,   -2000)]
        [TestCase(Money.Bill5000,   -55 )]
        [TestCase(Money.Bill10000,  -70  )]
        public void _釣り銭を補充する_正しくない枚数の場合(Money inMoney, int inCount) {
            var pool = new CreditPool();

            var role = new ChangePoolRole();

            Assert.Throws<Exception>(() => role.Append(pool, inMoney, inCount));
        }
    }
}

