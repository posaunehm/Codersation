using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using VendingMachine;

namespace VendingMachine.Test
{
    public class VendingMachineTest
    {
        private VendingMachine vm;

        [SetUp]
        public void SetUp()
        {
            vm = new VendingMachine();
        }

        [Test]
        public void 自販機に10円と50円を投入したら総計が60円になる()
        {
            vm.DropIn(Money.Ten);
            vm.DropIn(Money.Fifty);
            Assert.That(vm.Total, Is.EqualTo(60));
        }

        [Test]
        public void 自販機に50円と100円を投入したら総計が150円になる()
        {
            vm.DropIn(Money.Fifty);
            vm.DropIn(Money.OneHundred);
            Assert.That(vm.Total, Is.EqualTo(150));
        }

        [Test]
        public void 自販機に100円と500円を投入したら総計が600円になる()
        {
            vm.DropIn(Money.OneHundred);
            vm.DropIn(Money.FiveHundred);
            Assert.That(vm.Total, Is.EqualTo(600));
        }

        [Test]
        public void 自販機に500円と1000円札を投入したら総計が1500円になる()
        {
            vm.DropIn(Money.FiveHundred);
            vm.DropIn(Money.OneThousand);
            Assert.That(vm.Total, Is.EqualTo(1500));
        }

        [Test]
        public void 自販機に1000円と10円を投入して払い戻したらお釣りが1010円になって総計が0円になる()
        {
            vm.DropIn(Money.OneThousand);
            vm.DropIn(Money.Ten);
            vm.PayBack();
            Assert.That(vm.Total, Is.EqualTo(0));
            Assert.That(vm.Change, Is.EquivalentTo(new List<Money>() { Money.OneThousand, Money.Ten }));
        }

        [Test]
        public void 自販機に1円を投入したらお釣りが1円になって総計が0円になる()
        {
            vm.DropIn(Money.One);
            Assert.That(vm.Total, Is.EqualTo(0));
            Assert.That(vm.Change, Is.EquivalentTo(new List<Money>() { Money.One }));
        }

        [Test]
        public void 自販機に1円と5円を投入したらお釣りが6円になって総計が0円になる()
        {
            vm.DropIn(Money.One);
            vm.DropIn(Money.Five);
            Assert.That(vm.Total, Is.EqualTo(0));
            Assert.That(vm.Change, Is.EquivalentTo(new List<Money>() { Money.Five, Money.One }));
        }

        [Test]
        public void 自販機に5円と5000円を投入したらお釣りが5005円になって総計が0円になる()
        {
            vm.DropIn(Money.Five);
            vm.DropIn(Money.FiveThousand);
            Assert.That(vm.Total, Is.EqualTo(0));
            Assert.That(vm.Change, Is.EquivalentTo(new List<Money>() { Money.FiveThousand, Money.Five }));
        }

        [Test]
        public void 自販機に5000円と10000円を投入したらお釣りが15000円になって総計が0円になる()
        {
            vm.DropIn(Money.FiveThousand);
            vm.DropIn(Money.TenThousand);
            Assert.That(vm.Total, Is.EqualTo(0));
            Assert.That(vm.Change, Is.EquivalentTo(new List<Money>() { Money.TenThousand, Money.FiveThousand }));
        }

        [Test]
        public void 自販機に5円と10円を投入したらお釣りが5円になって総計が10円になる()
        {
            vm.DropIn(Money.Five);
            vm.DropIn(Money.Ten);
            Assert.That(vm.Total, Is.EqualTo(10));
            Assert.That(vm.Change, Is.EquivalentTo(new List<Money>() { Money.Five }));
        }

        [Test]
        public void 格納されているジュースの情報が取得できる()
        {
            var cokeStock = vm.Stock.Find(stock => stock.Name == Juice.NAME_Coke);
            Assert.That(cokeStock.Price, Is.EqualTo(120));
            Assert.That(cokeStock.Count, Is.EqualTo(5));

            var redbullStock = vm.Stock.Find(stock => stock.Name == Juice.NAME_RedBull);
            Assert.That(redbullStock.Price, Is.EqualTo(200));
            Assert.That(redbullStock.Count, Is.EqualTo(5));

            var waterStock = vm.Stock.Find(stock => stock.Name == Juice.NAME_Water);
            Assert.That(waterStock.Price, Is.EqualTo(100));
            Assert.That(waterStock.Count, Is.EqualTo(5));
        }

        [Test]
        public void お金を投入していないときはコーラを買えない()
        {
            Assert.That(vm.CanBuy(Juice.NAME_Coke), Is.False);
        }

        [Test]
        public void 自販機に120円を投入したときはコーラを買える()
        {
            vm.DropIn(Money.OneHundred);
            vm.DropIn(Money.Ten);
            vm.DropIn(Money.Ten);
            Assert.That(vm.CanBuy(Juice.NAME_Coke), Is.True);
        }

        [Test]
        public void 自販機に120円を投入してコーラを購入すると売上が120円になり在庫が4つになる()
        {
            vm.DropIn(Money.OneHundred);
            vm.DropIn(Money.Ten);
            vm.DropIn(Money.Ten);

            vm.Buy(Juice.NAME_Coke);

            Assert.That(vm.Stock.Find(stock => stock.Name == Juice.NAME_Coke).Count, Is.EqualTo(4));
            Assert.That(vm.Total, Is.EqualTo(0));
            Assert.That(vm.Sale, Is.EqualTo(120));
        }

        [Test]
        public void 自販機に100円を投入してコーラを購入しようとしても売上も在庫も変わらない()
        {
            vm.DropIn(Money.OneHundred);

            vm.Buy(Juice.NAME_Coke);

            Assert.That(vm.Stock.Find(stock => stock.Name == Juice.NAME_Coke).Count, Is.EqualTo(5));
            Assert.That(vm.Total, Is.EqualTo(100));
            Assert.That(vm.Sale, Is.EqualTo(0));
        }

        [Test]
        public void 自販機に1000円を投入してコーラを5つ購入した後はコーラを買えない()
        {
            vm.DropIn(Money.OneThousand);

            Enumerable.Range(1, 5).ToList().ForEach(i => vm.Buy(Juice.NAME_Coke));

            Assert.That(vm.CanBuy(Juice.NAME_Coke), Is.False);
        }

        [Test]
        public void 自販機に500円を投入してコーラを購入するとお釣りが380円になる()
        {
            vm.DropIn(Money.FiveHundred);

            vm.Buy(Juice.NAME_Coke);

            var changes = new List<AcceptableMoney>();
            changes.AddRange(Enumerable.Range(1, 3).Select(_ => Money.OneHundred));
            changes.Add(Money.Fifty);
            changes.AddRange(Enumerable.Range(1, 3).Select(_ => Money.Ten));
            Assert.That(vm.Change, Is.EqualTo(changes));
        }

        [Test]
        public void 自販機に500円を投入してコーラを購入するのを4回行うと4回目の購入が行われない()
        {
            Enumerable.Range(1, 3).ToList().ForEach(_ =>
            {
                vm.DropIn(Money.FiveHundred);
                vm.Buy(Juice.NAME_Coke);
            });

            Assert.That(vm.Sale, Is.EqualTo(360));
            vm.DropIn(Money.FiveHundred);
            Console.WriteLine(vm.Total);
            Console.WriteLine(vm.Change);
            vm.Buy(Juice.NAME_Coke);
            Console.WriteLine(vm.Total);
            Assert.That(vm.Change, Is.EquivalentTo(new List<Money>() { Money.FiveHundred }));
            Assert.That(vm.Sale, Is.EqualTo(360));
        }
    }
}
