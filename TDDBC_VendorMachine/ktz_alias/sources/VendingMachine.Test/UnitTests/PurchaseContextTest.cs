using System;
using System.Linq;

using Ninject;
using VendingMachine.Model;

using NUnit.Framework;

namespace VendingMachine.Test.Unit { 
    [TestFixture]
    public class _商品購入を扱うContextのTestSuite {
        private IKernel SetUpPurchaseContextKernel() {
            var kernel = new Ninject.StandardKernel();
            kernel.Bind<ChangePool>().ToMethod(ctx => TestHelper.InitInfinityReservedChange());
            kernel.Bind<ItemRackPosition>().ToMethod(ctx => TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase));
            kernel.Bind<IUserCoinMeckRole>().ToMethod(ctx => new CoinMeckRole());
            kernel.Bind<IUserPurchaseRole>().ToMethod(ctx => new ItemRackRole());
            kernel.Bind<PurchaseContext>().ToSelf();

            return kernel;
        }

        [Test]
        public void _金銭を投入するが購入せず排出する() {
            var ctx = this.SetUpPurchaseContextKernel().Get<PurchaseContext>();

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
            var ctx = this.SetUpPurchaseContextKernel().Get<PurchaseContext>();
            
            Assert.That(ctx.CanPurchase(0), Is.False);

            ctx.ReceiveMoney(Money.Coin100);                     
            ctx.ReceiveMoney(Money.Coin10);                     
            ctx.ReceiveMoney(Money.Coin10);      

            Assert.That(ctx.CanPurchase(0), Is.True, "should be purchased");

            var item = ctx.Purchase(0);
            Assert.That(item.Name, Is.EqualTo("Item0"));
            Assert.That(ctx.ReceivedTotal, Is.EqualTo(0));

            Assert.That(ctx.Eject().Any(), Is.False);
        }

        [Test]
        public void _金銭を投入して商品を受け取る() {

        }
    }
}

