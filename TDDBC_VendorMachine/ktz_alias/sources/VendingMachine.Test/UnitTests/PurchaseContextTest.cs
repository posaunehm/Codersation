using System;

using VendingMachine.Model;

using NUnit.Framework;

namespace VendingMachine.Test.Unit { 
    [TestFixture]
    public class _商品購入を扱うContextのTestSuite {
        [Test]
        public void _金銭を投入するが購入せず排出する() {
            var ctx = new PurchaseContext(
                TestHelper.InitInfinityReservedChange(),
                TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase)
            );

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
            var ctx = new PurchaseContext(
                TestHelper.InitInfinityReservedChange(),
                TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase)
            );

            Assert.That(ctx.CanPurchase(0), Is.False);

            ctx.ReceiveMoney(Money.Coin100);                     
            ctx.ReceiveMoney(Money.Coin10);                     
            ctx.ReceiveMoney(Money.Coin10);      

            Assert.That(ctx.CanPurchase(0), Is.True, "should be purchased");

            var item = ctx.Purchase(0);
            Assert.That(item.Name, Is.EqualTo("Item0"));
            Assert.That(ctx.ReceivedTotal, Is.EqualTo(0));
        }
    }
}

