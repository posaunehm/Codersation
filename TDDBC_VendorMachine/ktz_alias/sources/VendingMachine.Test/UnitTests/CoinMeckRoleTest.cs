using System;
using System.Collections.Generic;
using System.Linq;

using VendingMachine.Model;

using NUnit.Framework;

using TestUtils;

namespace VendingMachine.Test.Unit {
	[TestFixture]
	public class _利用者の入出金の関するTestSuite {
		[TestCase(Money.Coin10, MoneyStatus.Available, 10)]
		[TestCase(Money.Coin50, MoneyStatus.Available, 50)]
		[TestCase(Money.Coin100, MoneyStatus.Available, 100)]
		[TestCase(Money.Coin500, MoneyStatus.Available, 500)]
		[TestCase(Money.Bill1000, MoneyStatus.Available, 1000)]
		[TestCase(Money.Coin1, MoneyStatus.Unavailable, 1)]
		[TestCase(Money.Coin5, MoneyStatus.Unavailable, 5)]
		[TestCase(Money.Bill2000, MoneyStatus.Unavailable, 2000)]
		[TestCase(Money.Bill5000, MoneyStatus.Unavailable, 5000)]
		[TestCase(Money.Bill10000, MoneyStatus.Unavailable, 10000)]		
		[TestCase(Money.Unknown, MoneyStatus.Unavailable, 0)]
		public void _金種からお金への解決(Money inType, MoneyStatus inStatus, int inValue) {
			var money = MoneyResolver.Resolve(inType);
			
			Assert.That(money.Type, Is.EqualTo(inType));	
			Assert.That(money.Status, Is.EqualTo(inStatus));	
			Assert.That(money.Value, Is.EqualTo(inValue));	
		}
		
        [TestCase(10, Money.Coin10, MoneyStatus.Available)]
        [TestCase(50, Money.Coin50, MoneyStatus.Available)]
        [TestCase(100, Money.Coin100, MoneyStatus.Available)]
        [TestCase(500, Money.Coin500, MoneyStatus.Available)]
        [TestCase(1000, Money.Bill1000, MoneyStatus.Available)]
        [TestCase(1, Money.Coin1, MoneyStatus.Unavailable)]
        [TestCase(5, Money.Coin5, MoneyStatus.Unavailable)]
        [TestCase(2000, Money.Bill2000, MoneyStatus.Unavailable)]
        [TestCase(5000, Money.Bill5000, MoneyStatus.Unavailable)]
        [TestCase(10000, Money.Bill10000, MoneyStatus.Unavailable)]     
        [TestCase(0, Money.Unknown, MoneyStatus.Unavailable)]
        [TestCase(128, Money.Unknown, MoneyStatus.Unavailable)]
        [TestCase(-512, Money.Unknown, MoneyStatus.Unavailable)]
        public void _価値から金種への変換(int inValue, Money inType, MoneyStatus inStatus) {
            var money = MoneyResolver.Resolve(inValue);
            
            Assert.That(money.Type, Is.EqualTo(inType));    
            Assert.That(money.Status, Is.EqualTo(inStatus));    
        }

		[Test]
		public void _投入するお金の利用可不可を判定する() {
			var role = new CoinMeckRole();
			
			Assert.True(role.IsAvailableMoney(Money.Coin10), "10円は使用可能");
			Assert.True(role.IsAvailableMoney(Money.Coin50), "50円は使用可能");
			Assert.True(role.IsAvailableMoney(Money.Coin100), "100円は使用可能");
			Assert.True(role.IsAvailableMoney(Money.Coin500), "500円は使用可能");
			Assert.True(role.IsAvailableMoney(Money.Bill1000), "1000円は使用可能");

			Assert.False(role.IsAvailableMoney(Money.Coin1), "1円は使用不可");
			Assert.False(role.IsAvailableMoney(Money.Coin5), "5円は使用不可");
			Assert.False(role.IsAvailableMoney(Money.Bill2000), "2000円は使用不可");
			Assert.False(role.IsAvailableMoney(Money.Bill5000), "5000円は使用不可");
			Assert.False(role.IsAvailableMoney(Money.Bill10000), "10000円は使用不可");
		}
		
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void _利用者がお金を投入する(int inCount) {
			var role = new CoinMeckRole ();
			var received = new CashDeal();
			
            Assert.True(role.Receive(received, Money.Coin10, inCount)); 
            Assert.True(role.Receive(received, Money.Coin50, inCount));
            Assert.True(role.Receive(received, Money.Coin100, inCount));
            Assert.True(role.Receive(received, Money.Coin500, inCount));
            Assert.True(role.Receive(received, Money.Bill1000, inCount));
            Assert.False(role.Receive(received, Money.Coin1, inCount));
            Assert.False(role.Receive(received, Money.Coin5, inCount));
            Assert.False(role.Receive(received, Money.Bill2000, inCount));
            Assert.False(role.Receive(received, Money.Bill5000, inCount));
            Assert.False(role.Receive(received, Money.Bill10000, inCount));
            Assert.True(role.Receive(received, Money.Coin10, inCount)); 
            Assert.True(role.Receive(received, Money.Coin50, inCount));
            Assert.True(role.Receive(received, Money.Coin100, inCount));
            Assert.True(role.Receive(received, Money.Coin500, inCount));
            Assert.True(role.Receive(received, Money.Bill1000, inCount));
            Assert.False(role.Receive(received, Money.Coin1, inCount));
            Assert.False(role.Receive(received, Money.Coin5, inCount));
            Assert.False(role.Receive(received, Money.Bill2000, inCount));
            Assert.False(role.Receive(received, Money.Bill5000, inCount));
            Assert.False(role.Receive(received, Money.Bill10000, inCount));
			
			Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Coin1));
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Coin5));
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Bill2000));
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Bill5000));
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Bill10000));
			
            Assert.That(received.ChangedAount, Is.EqualTo(3320 * inCount));
		}
        
        [TestCase( 0)]
        [TestCase(-1)]
        [TestCase(-41)]
        public void _利用者がお金を投入する_正しくない投入枚数の場合(int inCount) {
            var role = new CoinMeckRole ();
            var received = new CashDeal();
            
            Assert.False(role.Receive(received, Money.Coin10, inCount)); 
            Assert.False(role.Receive(received, Money.Coin50, inCount));
            Assert.False(role.Receive(received, Money.Coin100, inCount));
            Assert.False(role.Receive(received, Money.Coin500, inCount));
            Assert.False(role.Receive(received, Money.Bill1000, inCount));
            Assert.False(role.Receive(received, Money.Coin1, inCount));
            Assert.False(role.Receive(received, Money.Coin5, inCount));
            Assert.False(role.Receive(received, Money.Bill2000, inCount));
            Assert.False(role.Receive(received, Money.Bill5000, inCount));
            Assert.False(role.Receive(received, Money.Bill10000, inCount));
            Assert.False(role.Receive(received, Money.Coin10, inCount)); 
            Assert.False(role.Receive(received, Money.Coin50, inCount));
            Assert.False(role.Receive(received, Money.Coin100, inCount));
            Assert.False(role.Receive(received, Money.Coin500, inCount));
            Assert.False(role.Receive(received, Money.Bill1000, inCount));
            Assert.False(role.Receive(received, Money.Coin1, inCount));
            Assert.False(role.Receive(received, Money.Coin5, inCount));
            Assert.False(role.Receive(received, Money.Bill2000, inCount));
            Assert.False(role.Receive(received, Money.Bill5000, inCount));
            Assert.False(role.Receive(received, Money.Bill10000, inCount));
            
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Coin1));
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Coin5));
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Bill2000));
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Bill5000));
            Assert.False(received.RecevedMoney.Credits.ContainsKey(Money.Bill10000));
            
            Assert.That(received.ChangedAount, Is.EqualTo(0));
        }

		[TestCase(Money.Coin100, 10)]
		[TestCase(Money.Coin500, 2)]
		[TestCase(Money.Bill1000, 1)]
		public void _何も購入せずお金を排出する(Money inMoney, int inRepeat) {
			var role = new CoinMeckRole ();
			var received = new CashDeal();
			var pool = TestHelper.InitInfinityReservedChange();
			
            role.Receive(received, inMoney, inRepeat);
			
            var expectReceived = received.RecevedMoney.TotalAmount();

			var changes = role.Eject(received, pool).Credits
                .Where(c => c.Value > 0)
				.ToDictionary(g => g.Key, g => g.Value)
			;
			
            Assert.That(received.RecevedMoney.TotalAmount(), Is.EqualTo(expectReceived));
			
			Assert.That(changes.Count, Is.EqualTo(1));
			Assert.True(changes.ContainsKey(inMoney));
			Assert.That(changes[inMoney], Is.EqualTo(inRepeat));
		}
		
		[Test]
		public void _お金を入れず購入() {
			var role = new CoinMeckRole ();
			var received = new CashDeal();
            var pool = new CreditPool();

            var changes = role.CalcChanges(received, pool, 100);
            Assert.That(changes.TotalAmount(), Is.EqualTo(0));	
		}
		
		public class _商品購入後お金を排出するParams {
			public class Parameter {
				public int Id {get; internal set;}
				public Tuple<Money, int>[] ReceivedMoney {get; internal set;}
				public Tuple<Money, int>[] ChangedMoney {get; internal set;}
			}
			
			public IEnumerable<Parameter> Source {
				get {
					var id = 1;
					
					yield return new Parameter {
						Id = id++,
						ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin100, 5)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin100, 10)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 5)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 2)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 3)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Bill1000, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {								
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Bill1000, 1)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {								
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Bill1000, 2)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Bill1000, 1), Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
				}
			}
		}
		
		[Test]
		public void _商品購入後お金を排出する(
			    [ValueSource(typeof(_商品購入後お金を排出するParams), "Source")] 
			    _商品購入後お金を排出するParams.Parameter inParameter) 
		{
			var role = new CoinMeckRole();
			var received = new CashDeal();
            var pool = TestHelper.InitInfinityReservedChange();
			
			foreach (var m in inParameter.ReceivedMoney) {
				for (var i = 0; i < m.Item2; ++i) {
					role.Receive (received, m.Item1, 1);
				}
			}
			
			var newReceives = new CashDeal(role.CalcChanges(received, pool, 100));			
            var changes = role.Eject(newReceives, pool)
                .Credits
                .Where(c => c.Value > 0)
				.ToDictionary(g => g.Key, g => g.Value)
			;
			
			var lookup = inParameter.ChangedMoney.ToDictionary(m => m.Item1, m => m.Item2);
			
			Assert.That(changes.Count, Is.EqualTo(lookup.Count), "count money type (id = {0})", inParameter.Id);
			
			foreach (var pair in changes) {
				Assert.True(lookup.ContainsKey(pair.Key), "money ({0}) should be contained (id = {1})", pair.Key, inParameter.Id);
				Assert.That(pair.Value, Is.EqualTo (lookup[pair.Key]), "money ({0}) count should be equaled (id = {1})", pair.Key, inParameter.Id);
			}
		}
	}
}

