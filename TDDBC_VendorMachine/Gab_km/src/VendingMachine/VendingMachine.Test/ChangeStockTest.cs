using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using VendingMachine;

namespace VendingMachine.Test
{
    class ChangeStockTest
    {
        [Test]
        public void hoge()
        {
            var changeStock = new ChangeStock(1, 1, 1, 1, 1);
            var change = new List<Money>();
            var price = 360;
            var result = changeStock.ReduceOneHundredAndAddChange(change, ref price);
            Assert.That(result,Is.False);
            var expected = new List<Money>() { Money.OneHundred };
            Assert.That(change, Is.EquivalentTo(expected));
            changeStock.CommitAll();
            result = changeStock.ReduceOneHundredAndAddChange(change, ref price);
            Assert.That(result, Is.True);
            Assert.That(change, Is.EquivalentTo(expected));
        }

        [Test]
        public void fuga()
        {
            var changeStock = new ChangeStock(0, 0, 3, 1, 1);
            var change = new List<Money>();
            var price = 360;
            var maybeChange = changeStock.GetChange(price);
            Assert.That(maybeChange.IsSome, Is.True);
            Assert.That(maybeChange.Value, Is.EquivalentTo(
                Enumerable.Range(1, 3).Select(_ => Money.OneHundred)
                .Concat(new List<Money>() { Money.Fifty })
                .Concat(new List<Money>() { Money.Ten })));
            maybeChange = changeStock.GetChange(price);
            Assert.That(maybeChange.IsSome, Is.False);
        }

        [Test]
        public void bar()
        {
            var changeStock = new ChangeStock(10, 10, 10, 10, 10);
            var change = new List<Money>();
            var price = 380;
            Enumerable.Range(1, 3).ToList().ForEach(i =>
                changeStock.GetChange(price));
            changeStock.CommitAll();

            var maybeChange = changeStock.GetChange(price);
            Assert.That(maybeChange.IsSome, Is.False);
            Assert.That(changeStock.Changes.Where(m => m.Value == 1000).Count(), Is.EqualTo(10));
            Assert.That(changeStock.Changes.Where(m => m.Value == 500).Count(), Is.EqualTo(10));
            Assert.That(changeStock.Changes.Where(m => m.Value == 100).Count(), Is.EqualTo(1));
            Assert.That(changeStock.Changes.Where(m => m.Value == 50).Count(), Is.EqualTo(7));
            Assert.That(changeStock.Changes.Where(m => m.Value == 10).Count(), Is.EqualTo(1));
        }
    }
}
