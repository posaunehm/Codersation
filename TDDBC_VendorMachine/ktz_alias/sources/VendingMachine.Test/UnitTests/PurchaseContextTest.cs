using System;
using System.Collections.Generic;
using System.Linq;

using Ninject;
using VendingMachine.Model;

using NUnit.Framework;

using TestUtils;

namespace VendingMachine.Test.Unit { 
    [TestFixture]
    public class _商品購入を扱うContextのTestSuite {
        [Test]
        public void _金銭を投入するが購入せず排出する() {
            var ctx = new Ninject.StandardKernel()
                .BindPurchaseContext()
                .Get<PurchaseContext>()
            ;

            ctx.ReceiveMoney(Money.Coin500);             
            Assert.That(ctx.ReceivedTotal, Is.EqualTo(500));

            ctx.ReceiveMoney(Money.Coin500);             
            Assert.That(ctx.ReceivedTotal, Is.EqualTo(1000));

            ctx.ReceiveMoney(Money.Coin100);             
            Assert.That(ctx.ReceivedTotal, Is.EqualTo(1100));

            var changes = ctx.Eject();
            Assert.That(changes.TotalAmount(), Is.EqualTo(1100));
            Assert.That(ctx.ReceivedTotal, Is.EqualTo(0));
        }

        [Test]
        public void _金銭を投入して商品を受け取る_丁度の場合() {
            var ctx = new Ninject.StandardKernel()
                .BindPurchaseContext()
                .Get<PurchaseContext>()
            ;

            Assert.That(ctx.Racks[0].State, Is.EqualTo(ItemRackState.CanNotPurchase));

            ctx.ReceiveMoney(Money.Coin100);                     
            ctx.ReceiveMoney(Money.Coin10);                     
            ctx.ReceiveMoney(Money.Coin10);      

            Assert.That(ctx.Racks[0].State, Is.EqualTo(ItemRackState.CanPurchase), "should be purchased");

            var item = ctx.Purchase(0);
            Assert.That(item.Name, Is.EqualTo("Item0"));
            Assert.That(ctx.ReceivedTotal, Is.EqualTo(0));

            Assert.That(ctx.Eject().Any(), Is.False);
        }

        [Test]
        public void _金銭を投入して商品を受け取る_釣り銭が発生する場合() {
            var ctx = new Ninject.StandardKernel()
                .BindPurchaseContext()
                .Get<PurchaseContext>()
            ;           

            Assert.That(ctx.Racks[0].State, Is.EqualTo(ItemRackState.CanNotPurchase));
            
            ctx.ReceiveMoney(Money.Coin100);                     
            ctx.ReceiveMoney(Money.Coin100);                     

            var item = ctx.Purchase(0);
            Assert.That(item.Name, Is.EqualTo("Item0"));
            Assert.That(ctx.ReceivedTotal, Is.EqualTo(80));

            var changes = ctx.Eject()
                .GroupBy(m => m)
                .ToDictionary(g => g.Key, g => g.Count())
            ;
            var expected = new Dictionary<Money, int> {
                {Money.Coin10, 3}, 
                {Money.Coin50, 1},
            };

            foreach (var m in expected) {
                Assert.That(changes.ContainsKey(m.Key), Is.True);
                Assert.That(changes[m.Key], Is.EqualTo(m.Value));
            }

            var notContained = Enum.GetValues(typeof(Money))
                .Cast<Money>()
                .Where(m => ! expected.ContainsKey(m))
            ;

            foreach (var m in notContained) {
                Assert.That(changes.ContainsKey(m), Is.False);
            }
        }
    }
}

